namespace ApplicationCore.Commons.Repository;

public interface IGenericRepository<T, in K> where T: IIdentity<K> where K : IComparable<K>
{

    T? FindById(K id);

    List<T> FindAll();
    T Add(T o);
    
    void RemoveById(K id);
    
    void Update(K id, T o);
    
    int SaveChanges();
}