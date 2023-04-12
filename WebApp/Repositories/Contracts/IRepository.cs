namespace WebApp.Repositories.Contracts;

public interface IRepository<TKey, TEntity>
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(TKey key);
    int Insert(TEntity entity);
    int Update(TEntity entity);
    int Delete(TKey key);
}
