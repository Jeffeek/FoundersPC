#region Using derectives

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace FoundersPC.Services.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll();
		IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression);
		void Create(TEntity entity);
		void Delete(TEntity entity);
		void Update(TEntity entity);
	}
}