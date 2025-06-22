using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VKR
{
    public static class GoogleDriveAuth
    {
        private const string CredentialsPath = "credentials.json";
        private const string TokensRoot = "googletokens";
        private static readonly string[] Scopes = new[]
        {
           DriveService.Scope.Drive, // ✅ Полный доступ к Google Drive
    "https://www.googleapis.com/auth/userinfo.email"
        };

        /// <summary>
        /// Проходит полную OAuth-авторизацию (открывает браузер), сохраняет токен в папку
        /// TokensRoot/{email} и возвращает готовый DriveService + email пользователя.
        /// </summary>
        public static async Task<(DriveService driveService, string email)> AuthorizeAndGetDriveServiceAsync()
        {
            // 1) Считываем client_id и client_secret
            ClientSecrets secrets;
            using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            }

            // 2) Генерируем локальный HTTP-порт для редиректа
            int port = GetFreePort();
            string redirectUri = $"http://localhost:{port}/";

            // 3) Формируем URL авторизации
            var authorizationUrl =
                "https://accounts.google.com/o/oauth2/v2/auth" +
                $"?client_id={secrets.ClientId}" +
                $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                "&response_type=code" +
                $"&scope={Uri.EscapeDataString(string.Join(" ", Scopes))}" +
                "&access_type=offline" +
                "&prompt=select_account";

            // 4) Открываем системный браузер
            Process.Start(new ProcessStartInfo
            {
                FileName = authorizationUrl,
                UseShellExecute = true
            });

            // 5) Ждём callback с кодом
            string code = await WaitForAuthCodeAsync(port);

            // 6) Обмениваем code → TokenResponse и сохраняем его временно
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = Scopes,
                DataStore = new FileDataStore("googletokens_temp", true)
            });

            TokenResponse token = await flow.ExchangeCodeForTokenAsync(
                userId: "user",
                code: code,
                redirectUri: redirectUri,
                taskCancellationToken: CancellationToken.None
            );

            // 7) Достаём email пользователя
            string email = await GetUserEmailAsync(token.AccessToken);

            // 8) Переносим токен в постоянное хранилище TokensRoot/{email}
            string userFolder = Path.Combine(TokensRoot, email);
            if (Directory.Exists(userFolder))
                Directory.Delete(userFolder, true);

            var store = new FileDataStore(userFolder, true);
            await store.StoreAsync("user", token);

            // 9) Собираем окончательный UserCredential и DriveService
            var credential = new UserCredential(flow, "user", token);
            var driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "VKR Cloud Manager"
            });

            return (driveService, email);
        }

        /// <summary>
        /// Восстанавливает DriveService из ранее сохранённого токена (без повторной авторизации).
        /// </summary>
        public static async Task<DriveService> CreateDriveServiceFromStoreAsync(string email)
        {
            // 1) Считываем client_id и client_secret
            ClientSecrets secrets;
            using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                secrets = GoogleClientSecrets.FromStream(stream).Secrets;
            }

            // 2) Открываем FileDataStore, где лежит токен
            string userFolder = Path.Combine(TokensRoot, email);
            var store = new FileDataStore(userFolder, true);

            // 3) Читаем сохранённый TokenResponse
            TokenResponse token = await store.GetAsync<TokenResponse>("user");

            // 4) Воссоздаём flow и credential
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = secrets,
                Scopes = Scopes,
                DataStore = store
            });
            var credential = new UserCredential(flow, "user", token);

            // 5) Возвращаем DriveService
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "VKR Cloud Manager"
            });
        }

        /// <summary>
        /// Возвращает строку вида "X.X GB / Y.Y GB" по занятым и общим квотам.
        /// </summary>
        public static async Task<string> GetDriveCapacityAsync(DriveService service)
        {
            var aboutReq = service.About.Get();
            aboutReq.Fields = "storageQuota";
            var about = await aboutReq.ExecuteAsync();

            double used = about.StorageQuota.Usage ?? 0;
            double total = about.StorageQuota.Limit ?? 0;
            return $"{used / 1e9:F1} GB / {total / 1e9:F1} GB";
        }

        private static int GetFreePort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static async Task<string> WaitForAuthCodeAsync(int port)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{port}/");
            listener.Start();

            var context = await listener.GetContextAsync();
            string code = context.Request.QueryString["code"];

            const string html = "<html><body><h2>Авторизация завершена. Можете закрыть окно.</h2></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(html);
            context.Response.ContentType = "text/html; charset=UTF-8";
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);

            listener.Stop();
            return code;
        }

        private static async Task<string> GetUserEmailAsync(string accessToken)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
                var resp = await http.GetAsync("https://www.googleapis.com/oauth2/v2/userinfo");
                var content = await resp.Content.ReadAsStringAsync();
                var obj = JObject.Parse(content);
                return obj["email"]?.ToString();
            }
        }
    }
}


