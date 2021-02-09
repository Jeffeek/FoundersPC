#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.UoW
{
	public class FoundersPCUnitOfWork : IUnitOfWorkAsync
	{
		private readonly DbContext _context;

		public FoundersPCUnitOfWork(DbContext context,
		                            ICPUsRepositoryAsync processorsRepository,
		                            IProducersRepositoryAsync producersRepository,
		                            IProcessorCoresRepositoryAsync processorCoresRepository,
		                            IGPUsRepositoryAsync videoCardsRepository,
		                            IVideoCardCoresRepositoryAsync videoCardCoresRepository,
		                            ICasesRepositoryAsync casesRepository,
		                            IHDDsRepositoryAsync hddsRepository,
		                            IMotherboardsRepositoryAsync motherboardsRepository,
		                            IPowerSuppliersRepositoryAsync powerSuppliersRepository,
		                            ISSDsRepositoryAsync ssdsRepository,
		                            IRAMsRepositoryAsync ramsRepository)
		{
			_context = context;
			ProcessorsRepository = processorsRepository;
			ProducersRepository = producersRepository;
			ProcessorCoresRepository = processorCoresRepository;
			VideoCardsRepository = videoCardsRepository;
			VideoCardCoresRepository = videoCardCoresRepository;
			CasesRepository = casesRepository;
			HDDsRepository = hddsRepository;
			MotherboardsRepository = motherboardsRepository;
			PowerSuppliersRepository = powerSuppliersRepository;
			SSDsRepository = ssdsRepository;
			RAMsRepository = ramsRepository;
		}

		#region Implementation of IUnitOfWork

		/// <inheritdoc />
		public ICPUsRepositoryAsync ProcessorsRepository { get; }

		/// <inheritdoc />
		public IProducersRepositoryAsync ProducersRepository { get; }

		/// <inheritdoc />
		public IProcessorCoresRepositoryAsync ProcessorCoresRepository { get; }

		/// <inheritdoc />
		public IGPUsRepositoryAsync VideoCardsRepository { get; }

		/// <inheritdoc />
		public IVideoCardCoresRepositoryAsync VideoCardCoresRepository { get; }

		/// <inheritdoc />
		public ICasesRepositoryAsync CasesRepository { get; }

		/// <inheritdoc />
		public IHDDsRepositoryAsync HDDsRepository { get; }

		/// <inheritdoc />
		public IMotherboardsRepositoryAsync MotherboardsRepository { get; }

		/// <inheritdoc />
		public IPowerSuppliersRepositoryAsync PowerSuppliersRepository { get; }

		/// <inheritdoc />
		public ISSDsRepositoryAsync SSDsRepository { get; }

		/// <inheritdoc />
		public IRAMsRepositoryAsync RAMsRepository { get; }

		/// <inheritdoc />
		public async Task<int> SaveChangesAsync()
		{ 
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch
			{
				return 0;
			}
		}

		#endregion
	}
}