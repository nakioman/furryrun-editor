using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FurryRun.Editor.Infrastructure.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        private const string ServicesNamespace = "FurryRun.Editor.Services";
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Types.FromThisAssembly()
                .InNamespace(ServicesNamespace, true)
                .WithServiceDefaultInterfaces()
                .LifestyleTransient());
        }
    }
}