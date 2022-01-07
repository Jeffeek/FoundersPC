using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Case;

public class DeleteRequestValidator : DeleteHardwareRequestValidator<DeleteRequest>
{
    public DeleteRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory) { }
}