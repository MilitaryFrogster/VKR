using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

public static class DropboxAuth
{
    public static async Task<(string AccessToken, string Email, string Capacity)> FullAuthorizeAsync()
    {
        string credentialsPath = "dropbox_credentials.json";
        if (!File.Exists(credentialsPath))
            throw new Exception("Файл dropbox_credentials.json не найден");

        var json = Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(credentialsPath));
        string clientId = json["client_id"].ToString();
        string clientSecret = json["client_secret"].ToString();
        string redirectUri = json["redirect_uri"].ToString();

        string authUrl = $"https://www.dropbox.com/oauth2/authorize?client_id={clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}";
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = authUrl,
            UseShellExecute = true
        });

        var http = new System.Net.HttpListener();
        http.Prefixes.Add(redirectUri);
        http.Start();
        var context = await http.GetContextAsync();
        var query = context.Request.Url.Query;
        var code = System.Web.HttpUtility.ParseQueryString(query).Get("code");

        const string html = "<html><body><h2>Dropbox авторизация завершена. Можете закрыть это окно.</h2></body></html>";
        var buffer = System.Text.Encoding.UTF8.GetBytes(html);
        context.Response.ContentLength64 = buffer.Length;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        context.Response.Close();
        http.Stop();

        using (var client = new System.Net.Http.HttpClient())
        {
            var tokenRequest = new System.Net.Http.FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri)
                });

            var response = await client.PostAsync("https://api.dropboxapi.com/oauth2/token", tokenRequest);
            var jsonText = await response.Content.ReadAsStringAsync();
            var tokenObj = Newtonsoft.Json.Linq.JObject.Parse(jsonText);
            var accessToken = tokenObj["access_token"]?.ToString();

            if (string.IsNullOrEmpty(accessToken))
                throw new Exception("Ошибка получения access_token");

            var (email, capacity) = await GetDropboxAccountInfoAsync(accessToken);
            return (accessToken, email, capacity);
        }
    }

    public static async Task<(string Email, string Capacity)> GetDropboxAccountInfoAsync(string accessToken)
    {
        using (var client = new System.Net.Http.HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var accResp = await client.PostAsync("https://api.dropboxapi.com/2/users/get_current_account", null);
            var accText = await accResp.Content.ReadAsStringAsync();
            var accJson = Newtonsoft.Json.Linq.JObject.Parse(accText);
            var email = accJson["email"].ToString();

            var spaceResp = await client.PostAsync("https://api.dropboxapi.com/2/users/get_space_usage", null);
            var spaceText = await spaceResp.Content.ReadAsStringAsync();
            var spaceJson = Newtonsoft.Json.Linq.JObject.Parse(spaceText);

            double used = spaceJson["used"]?.Value<double>() ?? 0;
            double allocated = spaceJson["allocation"]?["allocated"]?.Value<double>() ?? 0;

            string capacity = $"{used / (1024 * 1024 * 1024):F1} GB / {allocated / (1024 * 1024 * 1024):F1} GB";
            return (email, capacity);
        }
    }
}
