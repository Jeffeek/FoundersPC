using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IUsersEntrancesService
    {
        Task<IEnumerable<UserEntranceLog>> GetAll();

        Task<UserEntranceLog> Get(int id);

        Task<IEnumerable<UserEntranceLog>> GetEntrancesBetween(DateTime start, DateTime finish);

        Task<IEnumerable<UserEntranceLog>> GetEntrancesIn(DateTime date);
    }
}
