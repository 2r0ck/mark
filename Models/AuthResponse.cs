namespace DotNetGigs.Models
{
    public class AuthResponse
    {
        public string id{get;set;}

        public  string auth_token{get;set;}

        public double expires_in{get;set;}
    }
}