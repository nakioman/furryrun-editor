using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Castle.Facilities.TypedFactory;
using FurryRun.Editor.ViewModels;

namespace FurryRun.Editor.Infrastructure
{
    public class CastleBootstrapper<TRootViewModel> : BootstrapperBase
    {
        private ApplicationContainer _container;

        public CastleBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<TRootViewModel>();
        }

        protected override void Configure()
        {
            _container = new ApplicationContainer();
            _container.AddFacility<TypedFactoryFacility>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
                ? _container.Resolve(service)
                : _container.Resolve(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return (IEnumerable<object>)_container.ResolveAll(service);
        }

        protected override void BuildUp(object instance)
        {
            instance.GetType().GetProperties()
                .Where(property => property.CanWrite && property.PropertyType.IsPublic)
                .Where(property => _container.Kernel.HasComponent(property.PropertyType))
                .ForEach(property => property.SetValue(instance, _container.Resolve(property.PropertyType), null));
        }
    }
}