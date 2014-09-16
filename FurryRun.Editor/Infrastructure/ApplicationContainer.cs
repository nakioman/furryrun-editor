using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace FurryRun.Editor.Infrastructure
{
    public class ApplicationContainer : WindsorContainer
    {
        public ApplicationContainer()
        {
            Register(
                Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifeStyle.Is(LifestyleType.Singleton),
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Is(LifestyleType.Singleton)
                );

            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            Register(Types.FromAssembly(GetType().Assembly)
                .Where(x => x.Name.EndsWith("ViewModel"))
                .Configure(x => x.LifeStyle.Is(LifestyleType.Transient)));
        }
    }
}