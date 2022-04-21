namespace DigitalFolder.Configuration
{
    public class TokenSettings
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
