namespace Kurzy.Services.Interfaces
{
    public interface IGenericSearchService
    {
        T? FindEntity<T>(object input, string model = "none") where T : class;
    }
}
