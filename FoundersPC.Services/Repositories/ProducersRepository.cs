using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Repositories
{
    public class ProducersRepository : IProducersRepository
    {
	    private readonly DbContext _context;

	    #region Implementation of IRepository<Producer>

	    public ProducersRepository(DbContext context)
	    {
		    _context = context;
	    }

	    public async Task<Producer> GetAsync(int? id)
        {
	        if (!id.HasValue) return null;
	        var producer = await _context.FindAsync<Producer>(id);
	        return producer;
		}

	    public async Task<IEnumerable<Producer>> GetAllAsync() => await _context.Set<Producer>().ToListAsync();

	    public Task AddAsync(Producer entity)
	    {
		    throw new NotImplementedException();
	    }

	    public Task RemoveAsync(Producer entity)
	    {
		    throw new NotImplementedException();
	    }

	    public Task UpdateAsync(Producer entity)
	    {
		    throw new NotImplementedException();
	    }

	    #endregion
    }
}
