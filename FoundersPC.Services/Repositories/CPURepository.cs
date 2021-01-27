using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Repositories
{
    public class CPURepository : ICPURepository
    {
	    private DbContext _context;

	    public CPURepository(DbContext context) => _context = context;

	    public async Task<IEnumerable<CPU>> GetAllAsync() => await _context.Set<CPU>().ToListAsync();

	    #region Implementation of IRepository<CPU>

	    public async Task<CPU> GetAsync(int? id)
	    {
		    if (!id.HasValue) return null;
		    var cpu = await _context.FindAsync<CPU>(id);
		    return cpu;
	    }

	    public async Task AddAsync(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    public async Task RemoveAsync(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    public async Task UpdateAsync(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    #endregion
    }
}
