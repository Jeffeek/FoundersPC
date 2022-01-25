﻿using System;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetAllQuery : Query<Domain.Entities.Hardware.Producer>
{
    public string? SearchText { get; set; }

    public override Expression<Func<Domain.Entities.Hardware.Producer, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (!String.IsNullOrEmpty(SearchText))
            SearchText.Split()
                      .ForEach(term =>
                               {
                                   result = result.And(x => x.FullName.Contains(term)
                                                            || x.ShortName != null && x.ShortName.Contains(term)
                                                            || x.Website != null && x.Website.Contains(term));
                               });

        return result;
    }
}