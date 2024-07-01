namespace CenterEnglishManagement.Service.IService
{
    public interface ICommonServices<T> where T: class
    {
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(int id,T entity);
        Task DeleteAsync(int id);
        Task<T> GetbyIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc);


    }
}
