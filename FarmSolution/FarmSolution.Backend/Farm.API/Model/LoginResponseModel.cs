namespace Farm.API.Model
{
    public class LoginResponseModel
    {
        public LoginResponseModel(string token)
        {
            Token = token;
        }

        public string Token { get; set; } 
    }
}
