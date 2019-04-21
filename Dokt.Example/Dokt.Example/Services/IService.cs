namespace Dokt.Example.Services
{
    public interface IService
    {
        string GetResult(int id);
        string PostContent(string content);
    }
}