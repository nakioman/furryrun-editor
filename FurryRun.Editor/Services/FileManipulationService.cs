using System;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Castle.Core.Logging;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public class FileManipulationService : IFileManipulationService
    {
        public ILogger Logger { get; set; }

        public Stage ImportGlitchLocationFile(string filename)
        {
            var gameObject = DeserializeGlitchLocationFile(filename);
            return MapGlitchLocationFileToModel(gameObject);
        }

        private Stage MapGlitchLocationFileToModel(game_object gameObject)
        {
            var stage = new Stage
            {
                Left = Convert.ToInt32(gameObject.@object.Single().@int.Single(x => x.id == "l").Value),
                Right = Convert.ToInt32(gameObject.@object.Single().@int.Single(x => x.id == "r").Value),
                Top = Convert.ToInt32(gameObject.@object.Single().@int.Single(x => x.id == "t").Value),
                Bottom = Convert.ToInt32(gameObject.@object.Single().@int.Single(x => x.id == "b").Value),
                Id = gameObject.@object.Single().str.Single(x => x.id == "tsid").Value,
                Label = gameObject.@object.Single().str.Single(x => x.id == "label").Value,
            };

            var gradient = gameObject.@object.Single().object1.SingleOrDefault(x => x.id == "gradient");
            if (gradient != null)
            {
                var bottom = System.Drawing.ColorTranslator.FromHtml("#" + gradient.str.Single(x => x.id == "bottom").Value);
                stage.BottomGradientColor = Color.FromArgb(bottom.A, bottom.R, bottom.G, bottom.B);
                var top = System.Drawing.ColorTranslator.FromHtml("#" + gradient.str.Single(x => x.id == "top").Value);
                stage.TopGradientColor = Color.FromArgb(top.A, top.R, top.G, top.B);
            }

            return stage;
        }

        private game_object DeserializeGlitchLocationFile(string filename)
        {
            try
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var xmlReader = XmlReader.Create(stream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(game_object));
                        return (game_object)xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("There was an error trying to import glitch location file", ex);
                throw;
            }
        }
    }
}