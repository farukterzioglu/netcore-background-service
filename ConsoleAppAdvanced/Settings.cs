using System.Collections.Generic;

namespace ConsoleApp
{
    public class ServerSettings
    {
        public List<string> AvailableServers {get;set;}
    }

    public class ExternalSettings
    {
        public int SomeValue { get; set; }
        public string AnotherValue { get; set; }
    }

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