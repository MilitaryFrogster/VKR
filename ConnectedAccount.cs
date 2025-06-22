namespace VKR
{
    public class ConnectedAccount
    {
        public string Email { get; set; }
        public string Cloud { get; set; }
        public string Capacity { get; set; }

        // Добавлено: свойство AccessToken
        public string AccessToken { get; set; }

        public override string ToString() => $"{Email} - {Cloud} - {Capacity}";
    }
}