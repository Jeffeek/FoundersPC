using System;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.SharedKernel.Extensions;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetAllQuery : GetAllHardwareQuery<Domain.Entities.Hardware.Case>
{
    public override Expression<Func<Domain.Entities.Hardware.Case, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (!String.IsNullOrEmpty(SearchText))
            SearchText.Split()
                      .ForEach(term =>
                               {
                                   result = result.And(x => x.Metadata.Title.Contains(term)
                                                            || x.Metadata.Producer.FullName.Contains(term)
                                                            || x.Metadata.Producer.ShortName!.Contains(term)
                                                            || x.Metadata.HardwareType.Name.Contains(term)
                                                            || x.Metadata.Title.Contains(term)
                                                            || x.Metadata.CaseType!.Name.Contains(term)
                                                            || x.Metadata.Color!.Name.Contains(term)
                                                            || x.Metadata.Material!.Name.Contains(term)
                                                            || x.Metadata.WindowMaterial!.Name.Contains(term));
                               });

        return result;
    }
}