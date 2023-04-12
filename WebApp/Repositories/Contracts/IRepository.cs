namespace WebApp.Repositories.Contracts;

interface IRepository<Key, Entity> where Entity: class
{
    IEnumerable<Entity> GetAll();
    Entity? GetById(Key key);
    IEnumerable<Entity> Search(string search);
    int Insert(Entity entity);
    int Update(Entity entity);
    int Delete(Key key);
}
