using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Castle.Core.Logging;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public class FileManipulationService : IFileManipulationService
    {
        private readonly IGlitchLocationMappingService _mappingService;
        private ILogger _logger = NullLogger.Instance;
        private const string OptionsFileName = "options.xml";
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public FileManipulationService(IGlitchLocationMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public Stage ImportGlitchLocationFile(string filename)
        {
            var gameObject = DeserializeGlitchLocationFile(filename);
            var stage = _mappingService.MapGlitchLocationFileToModel(gameObject);
            return stage;
        }



        public game_object DeserializeGlitchLocationFile(string filename)
        {
            try
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var xmlReader = XmlReader.Create(stream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(game_object));
                        try
                        {
                            return (game_object)xmlSerializer.Deserialize(xmlReader);
                        }
                        catch (InvalidOperationException exception)
                        {
                            Logger.Error("There was an error trying to import the file, the file seems not to be a glitch location", exception);
                            throw;
                        }
                    }
                }
            }
            catch (IOException exception)
            {
                Logger.Error("There was an error trying to load the glitch location file", exception);
                throw;
            }
        }

        public static Options LoadOptions()
        {
            try
            {
                using (var stream = new FileStream(OptionsFileName, FileMode.Open, FileAccess.Read))
                {
                    using (var xmlReader = XmlReader.Create(stream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(Options));
                        return (Options)xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception)
            {
                return new Options();
            }
        }

        public void SaveOptions(Options options)
        {
            using (var stream = new FileStream(OptionsFileName, FileMode.Create, FileAccess.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(Options));
                xmlSerializer.Serialize(stream, options);
            }
        }

        public void SaveFurryRunFile(string fileName, Stage stage)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(Stage));
                xmlSerializer.Serialize(stream, stage);
            }
        }

        public Stage LoadFurryRunFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var xmlSerializer = new XmlSerializer(typeof(Stage));
                return (Stage) xmlSerializer.Deserialize(stream);
            }
        }
    }
}