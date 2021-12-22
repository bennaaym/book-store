using System.Collections.Generic;



namespace Book_Store.Models.DataLayer.Repositories
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> List(QueryOptions<T> options);
    int Count { get; }

    //overload Get method
    T Get(QueryOptions<T> options);
    T Get(int id);
    T Get(string id);

    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);

    void Save();
  }
}
