using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoundersPC.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Services.Repositories
{
    public class CPURepository : ICPURepository
    {
	    private DbContext _context;

	    public CPURepository(DbContext context) => _context = context;

	    public IEnumerable<CPU> GetAll()
	    {
			_context.Set<ChipProducer>().Load();
			_context.Set<CrystalSerial>().Load();
			_context.Set<CPU>().Load();
		    return _context.Set<CPU>().ToList();
	    }

	    #region Implementation of IRepository<CPU>

	    /// <inheritdoc />
	    public CPU Get(int? id) => throw new NotImplementedException();

	    /// <inheritdoc />

	    /// <inheritdoc />
	    public void Add(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void Remove(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    /// <inheritdoc />
	    public void Update(CPU entity)
	    {
		    throw new NotImplementedException();
	    }

	    #endregion
    }
}
