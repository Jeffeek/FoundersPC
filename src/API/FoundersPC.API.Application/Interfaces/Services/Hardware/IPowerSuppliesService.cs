﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IPowerSuppliesService : IPaginateableService<PowerSupplyReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="PowerSupplyReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliesAsync();

        Task<PowerSupplyReadDto> GetPowerSupplyByIdAsync(int powerSupplyId);

        Task<bool> CreatePowerSupplyAsync(PowerSupplyInsertDto powerSupply);

        Task<bool> UpdatePowerSupplyAsync(int id, PowerSupplyUpdateDto powerSupply);

        Task<bool> DeletePowerSupplyAsync(int id);
    }
}