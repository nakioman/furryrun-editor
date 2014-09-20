using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace FurryRun.Editor.Infrastructure
{
    public interface ICustomWindowManager
    {
        Window GetWindow(object rootModel, object context = null, IDictionary<string, object> settings = null);
        // Summary:
        //     Shows a modal dialog for the specified model.
        //
        // Parameters:
        //   rootModel:
        //     The root model.
        //
        //   context:
        //     The context.
        //
        //   settings:
        //     The optional dialog settings.
        //
        // Returns:
        //     The dialog result.
        bool? ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null);
        //
        // Summary:
        //     Shows a popup at the current mouse position.
        //
        // Parameters:
        //   rootModel:
        //     The root model.
        //
        //   context:
        //     The view context.
        //
        //   settings:
        //     The optional popup settings.
        void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null);
        //
        // Summary:
        //     Shows a non-modal window for the specified model.
        //
        // Parameters:
        //   rootModel:
        //     The root model.
        //
        //   context:
        //     The context.
        //
        //   settings:
        //     The optional window settings.
        void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null);
    }
}