using System;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.SharedKernel.Extensions;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class GetAllQuery : GetAllHardwareQuery<Domain.Entities.Hardware.Processor>
{
    public override Expression<Func<Domain.Entities.Hardware.Processor, bool>> GetExpression()
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
                                                            || x.Metadata.Series!.Contains(term)
                                                            || x.Metadata.Socket!.Name.Contains(term)
                                                            || x.Metadata.IntegratedGraphics!.Metadata.Title.Contains(term)
                                                            || x.Metadata.TechProcess!.Name.Contains(term));
                               });

        return result;
    }
}