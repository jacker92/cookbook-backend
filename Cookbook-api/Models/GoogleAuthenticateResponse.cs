namespace Cookbook_api.Services
{
    public class GoogleAuthenticateResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Given_Name { get; set; }
        public string Family_Name { get; set; }
        public bool Email_Verified { get; set; }
    }
}