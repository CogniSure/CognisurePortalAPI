namespace Portal.Repository.Login
{
    public interface ILoginRepository
    {
        string GetToken(); 
    }
    public class LoginRepository : ILoginRepository
    {
        public string  GetToken()
        {
            return "This is valid token";
        }
    }
}