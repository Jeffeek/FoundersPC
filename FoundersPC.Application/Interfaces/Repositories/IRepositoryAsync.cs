﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersPC.Application.Interfaces.Repositories
{
	public interface IRepositoryAsync<T> where T : class
	{
		Task<T> AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(T entity);

		Task<T> GetByIdAsync(int id);

		Task<IEnumerable<T>> GetAllAsync();

		Task<bool> AnyAsync(T entity);
	}
}
