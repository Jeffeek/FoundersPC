using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using FoundersPC.Services.Models;
using FoundersPC.Services.Repositories;
using Ninject.Modules;

namespace FoundersPC.API.Utilities
{
    public class AutofacConfig : NinjectModule
    {
	    #region Overrides of NinjectModule

	    /// <inheritdoc />
	    public override void Load()
	    {
		    //Bind<ICPURepository<CPU>>().To<CPURepository>();
	    }

	    #endregion
    }
}
