using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public interface IFileManipulationService
    {
        Stage ImportGlitchLocationFile(string filename);
    }
}