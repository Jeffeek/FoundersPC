﻿#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.ApplicationShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU
{
    public interface IVideoCardCoresRepositoryAsync : IRepositoryAsync<VideoCardCore> { }
}