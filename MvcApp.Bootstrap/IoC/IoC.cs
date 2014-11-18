using System;
using MvcApp.BusinessLogic.Managers.Implementations;
using MvcApp.BusinessLogic.Managers.Interfaces;
using MvcApp.BusinessLogic.Repositories.Implementations;
using MvcApp.BusinessLogic.Repositories.Interfaces;
using MvcApp.Dal.Contexts;
using StructureMap;
using StructureMap.Graph;

namespace MvcApp.Bootstrap.IoC
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

                // Base
                x.For<IBaseRepository>().Use<BaseRepository>();
                x.For<MvcApp.Dal.Interfaces.IContext>().Singleton().Use<MvcContext>();

                // Managers
                x.For<ICrudManager>().Use<CrudManager>();
                x.For<ITeacherManager>().Use<TeacherManager>();
                x.For<ICreateWithAdditionalManager>().Use<CreateWithAdditionalManager>();
            });
            return ObjectFactory.Container;
        }

        public static object GetInstance(Type type)
        {
            return ObjectFactory.GetInstance(type);
        }
    }
}
