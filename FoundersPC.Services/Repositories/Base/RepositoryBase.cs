#region Using derectives

using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.Base
{
	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected RepositoryBase(DbContext repositoryContext) => RepositoryContext = repositoryContext;
		protected DbContext RepositoryContext { get; }

		public IQueryable<TEntity> GetAll() => RepositoryContext.Set<TEntity>().AsNoTracking();

		public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression) => RepositoryContext
			.Set<TEntity>()
			.Where(expression)
			.AsNoTracking();

		public void Create(TEntity entity)
		{
			RepositoryContext.Set<TEntity>().Add(entity);
		}

		public void Update(TEntity entity)
		{
			RepositoryContext.Set<TEntity>().Update(entity);
		}

		public void Delete(TEntity entity)
		{
			RepositoryContext.Set<TEntity>().Remove(entity);
		}
	}
}