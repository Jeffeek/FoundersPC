﻿#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.ApplicationShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware

{
    public interface ICasesRepositoryAsync : IRepositoryAsync<Case> { }
}