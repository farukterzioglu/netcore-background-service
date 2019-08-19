namespace ConsoleApp
{
    public class AuthenticationSettings 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ApplicationSettings
    {
        public string Environment { get; set; }
        public int IntervalMs { get; set; }
    }
}