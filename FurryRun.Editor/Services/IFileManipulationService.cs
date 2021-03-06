﻿using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public interface IFileManipulationService
    {
        Stage ImportGlitchLocationFile(string filename);
        game_object DeserializeGlitchLocationFile(string filename);
        void SaveOptions(Options options);
        void SaveFurryRunFile(string fileName, Stage stage);
        Stage LoadFurryRunFile(string fileName);
    }
}