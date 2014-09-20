using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FurryRun.Editor.Infrastructure.Installers
{
    public class CaliburnInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               Component.For(typeof (ICustomWindowManager), typeof (IWindowManager)).ImplementedBy<CustomWindowManager>().LifeStyle.Is(LifestyleType.Singleton),
               Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Is(LifestyleType.Singleton)
               );
        }
    }
}