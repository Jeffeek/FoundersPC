using FluentValidation;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Producer;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public UpdateRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
}