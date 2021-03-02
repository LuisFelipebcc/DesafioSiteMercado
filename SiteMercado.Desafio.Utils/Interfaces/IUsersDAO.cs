namespace SiteMercado.Desafio.Utils.Interfaces
{
    public interface IUsersDAO
    {
        dynamic Find(string userID, string accessKey);
    }
}
