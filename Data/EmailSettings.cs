namespace SASTE.Data // ou SASTE.Services (depende da pasta)
{
    public class EmailSettings
    {
        public string From { get; set; } = "";
        public string SmtpServer { get; set; } = "";
        public int Port { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}