namespace API.Repository.Interface;

public interface IRepository<TEntity,T>
{
    public IEnumerable<TEntity> Get();
    public TEntity Get(T id);
    public int Insert(TEntity entity);
    public int Update(TEntity entity);
    public int Delete(T id);
}

