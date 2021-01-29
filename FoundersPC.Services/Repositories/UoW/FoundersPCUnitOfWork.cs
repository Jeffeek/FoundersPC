#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Services.Repositories.CPU;
using FoundersPC.Services.Repositories.ProcessorLineup;
using FoundersPC.Services.Repositories.Producer;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.UoW
{
	public class FoundersPCUnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;

		public FoundersPCUnitOfWork(DbContext context,
		                            ICPUsRepository cpuRepository,
		                            IProducersRepository producersRepository,
		                            IProcessorLineupsRepository processorLineupsRepository)
		{
			_context = context;
			ProcessorsRepository = cpuRepository;
			ProducersRepository = producersRepository;
			ProcessorLineupsRepository = processorLineupsRepository;

			//ProcessorsRepository = new CPUsRepository(context);
			//ProducersRepository = new ProducersRepository(context);
			//ProcessorLineupsRepository = new ProcessorLineupsRepository(context);
		}

		#region Implementation of IUnitOfWork

		/// <inheritdoc />
		public ICPUsRepository ProcessorsRepository { get; }

		/// <inheritdoc />
		public IProducersRepository ProducersRepository { get; }

		/// <inheritdoc />
		public IProcessorLineupsRepository ProcessorLineupsRepository { get; }

        /// <inheritdoc />
		public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() >= 0;

		#endregion
	}
}