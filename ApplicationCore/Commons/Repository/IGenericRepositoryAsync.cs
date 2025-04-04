namespace ApplicationCore.Commons.Repository;

public interface IGenericRepositoryAsync<T, in K> where T: IIdentity<K> where K : IComparable<K>
{
    Task<T?> FindByIdAsync(K id);
    
    Task<List<T>> FindAllAsync();
    
    Task RemoveByIdAsync(K id);
    
    Task UpdateAsync(K id, T o);
    
    Task<int> SaveChangesAsync();
}