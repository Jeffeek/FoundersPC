using System;
using System.Collections.Generic;
using System.Text;

namespace FoundersPC.Services.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
	    TEntity Get(int? id);
	    IEnumerable<TEntity> GetAll();
	    void Add(TEntity entity);
	    void Remove(TEntity entity);
	    void Update(TEntity entity);
    }
}
