using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace FurryRun.Editor.Infrastructure
{
    public class CustomWindowManager : WindowManager, ICustomWindowManager
    {
        public Window GetWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            var window = CreateWindow(rootModel, false, context, settings);
            return window;
        }
    }
}