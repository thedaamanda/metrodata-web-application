using WebApp.Models;

namespace WebApp.Repositories.Contracts;

interface IRepository<Key, Entity> where Entity: class
{
    IEnumerable<Entity> GetAll();
    Entity? GetById(Key id);
    IEnumerable<Entity> Search(string name);
    int Insert(Entity entity);
    int Update(Entity entity);
    int Delete(Key id);
}
