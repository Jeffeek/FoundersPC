using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class UpdateRequestValidator : UpdateHardwareRequestValidator<UpdateRequest>
{
    public UpdateRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory) { }
}