#region Using derectives

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories
{
	public class FoundersPCUnitOfWork : IUnitOfWork
	{
		private DbContext _context;

		public FoundersPCUnitOfWork(DbContext context)
		{
			ProcessorsRepository = new CPURepository(context);
			ProducersRepository = new ProducersRepository(context);
		}

		#region Implementation of IUnitOfWork

		/// <inheritdoc />
		public ICPURepository ProcessorsRepository { get; }

		/// <inheritdoc />
		public IProducersRepository ProducersRepository { get; }

		/// <inheritdoc />
		public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

		#endregion
	}
}