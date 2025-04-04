using ApplicationCore.Commons.Repository;
using Infrastructure.Memory.Generators;

namespace Infrastructure.Memory.Repositories;

public class MemoryGenericRepository<T, K>:IGenericRepository<T, K> where T: class, IIdentity<K> where K : IComparable<K>
{
    private readonly IGenericGenerator<K>? _idGenerator;

    public MemoryGenericRepository(IGenericGenerator<K> idGenerator = null)
    {
        _idGenerator = idGenerator;
    }


    private Dictionary<K, T> _data = new();
    

    public Task<T?> FindByIdAsync(K id)
    {
        return Task.FromResult(_data.ContainsKey(id) ? _data[id] : null);
    }

    public Task<List<T>> FindAllAsync()
    {
        return Task.FromResult(_data.Values.ToList());
    }

    public T? FindById(K id)
    {
        try
        {
            return _data[id];
        }
        catch(KeyNotFoundException)
        {
            return null;
        }
    }

    public List<T> FindAll()
    {
        return _data.Values.ToList();
    }

    public T Add(T entity)
    {
        if (_idGenerator != null)
        {
            entity.Id = _idGenerator.Next;
        }
        _data[entity.Id]= entity;
        return entity;
    }

    public void RemoveById(K id)
    {
        _data.Remove(id);
    }

    public void Update(K id, T o)
    {
        if (_data.ContainsKey(id))
        {
            _data[id] = o;
        }
    }

    public int SaveChanges()
    {
        throw new NotImplementedException();
    }
}