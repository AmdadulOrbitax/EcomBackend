using MongoDB.Driver;

namespace E_commerce.Interface
{
    public interface IDatabaseService<T> where T : class
    {

        public Task SetCollection(string collectionName);
        public Task<List<T>> GetAllAsync();

        public Task<T> FindAsync(string id);
        public Task<T> FindAsync(string userName, string password);

        public Task AddAsync(T newData);


        public Task UpdateAsync(string id, T newData);

        public Task DeleteAsync(string id);

    }
}