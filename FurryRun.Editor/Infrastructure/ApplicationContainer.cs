using Caliburn.Micro;
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FurryRun.Editor.Infrastructure
{
    public class ApplicationContainer : WindsorContainer
    {
        public ApplicationContainer()
        {
            Install(FromAssembly.This());
        }
    }
}