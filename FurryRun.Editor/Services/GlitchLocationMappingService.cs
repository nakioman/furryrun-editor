using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Castle.Core.Logging;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public class GlitchLocationMappingService : IGlitchLocationMappingService
    {
        private ILogger _logger = NullLogger.Instance;
        public ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        public Stage MapGlitchLocationFileToModel(game_object gameObject)
        {
            var stage = MapGlitchObjectToStage(gameObject.@object.Single());
            stage.Layers = MapGlitchLayers(gameObject.@object.Single().object1.Single(x => x.id == "layers"));
            return stage;
        }

        public SortedList<int, Layer> MapGlitchLayers(@object layerObj)
        {
            var list = new SortedList<int, Layer>();
            foreach (var obj in layerObj.object1)
            {
                var layer = new Layer
                {
                    ZIndex = Convert.ToInt32(obj.@int.Single(x => x.id == "z").Value),
                    Name = obj.@str.Single(x => x.id == "name").Value,
                    Filters = MapGlitchFilters(obj.object1.Single(x => x.id == "filtersNEW")),
                    Height = Convert.ToInt32(obj.@int.Single(x => x.id == "h").Value),
                    Width = Convert.ToInt32(obj.@int.Single(x => x.id == "w").Value)
                };
                layer.Items = MapGlitchItems(obj.object1.Single(x => x.id == "decos"), layer);
                list.Add(layer.ZIndex, layer);
            }
            return list;
        }

        public SortedList<int, LayerItem> MapGlitchItems(@object decos, Layer layer)
        {
            var items = new SortedList<int, LayerItem>();
            foreach (var obj in decos.object1)
            {
                var item = new LayerItem
                {
                    Height = Convert.ToInt32(obj.@int.Single(x => x.id == "h").Value),
                    Width = Convert.ToInt32(obj.@int.Single(x => x.id == "w").Value),
                    X = Convert.ToInt32(obj.@int.Single(x => x.id == "x").Value),
                    Y = Convert.ToInt32(obj.@int.Single(x => x.id == "y").Value),
                    ZIndex = Convert.ToInt32(obj.@int.Single(x => x.id == "z").Value),
                    Name = obj.str.Single(x => x.id == "name").Value,
                    SpriteClass = obj.str.Single(x => x.id == "sprite_class").Value,
                    Layer = layer,
                };
                var rotate = obj.@int.SingleOrDefault(x => x.id == "r");
                if (rotate != null)
                {
                    item.Rotate = Convert.ToInt32(rotate.Value);
                }
                if (obj.@bool != null)
                {
                    var flip = obj.@bool.SingleOrDefault(x => x.id == "h_flip");
                    if (flip != null)
                    {
                        item.Flip = Convert.ToBoolean(flip.Value);
                    }
                }

                items.Add(item.ZIndex, item);
            }
            return items;
        }

        public IList<Filter> MapGlitchFilters(@object objs)
        {
            var filters = new List<Filter>();
            foreach (var obj in objs.object1)
            {
                var filter = new Filter();
                var filterValue = obj.@int.Single(x => x.id == "value").Value;
                var value = Convert.ToInt32(filterValue);
                if (value < 0)
                {
                    filter.Value = 1 - (value / -100);
                }
                else
                {
                    filter.Value = 1 + (value / 100);
                }

                //TODO Apply value to tintColor filter
                //TODO Apply value to tintAmount filter
                //TODO Apply value to blur filter
                switch (obj.id)
                {
                    case "brightness":
                        filter.Type = FilterType.Brightness;
                        break;
                    case "contrast":
                        filter.Type = FilterType.Contrast;
                        break;
                    case "saturation":
                        filter.Type = FilterType.Saturation;
                        break;
                    default:
                        var error = String.Format("The filter {0}, is not implemented yet!", obj.id);
                        Logger.Error(error);
                        break;
                }
                filters.Add(filter);
            }
            return filters;
        }

        public Stage MapGlitchObjectToStage(@object obj)
        {
            var stage = new Stage
            {
                Left = Convert.ToInt32(obj.@int.Single(x => x.id == "l").Value),
                Right = Convert.ToInt32(obj.@int.Single(x => x.id == "r").Value),
                Top = Convert.ToInt32(obj.@int.Single(x => x.id == "t").Value),
                Bottom = Convert.ToInt32(obj.@int.Single(x => x.id == "b").Value),
                Id = obj.str.Single(x => x.id == "tsid").Value,
                Label = obj.str.Single(x => x.id == "label").Value,
            };

            var gradient = obj.object1.SingleOrDefault(x => x.id == "gradient");
            if (gradient != null)
            {
                var bottom = System.Drawing.ColorTranslator.FromHtml("#" + gradient.str.Single(x => x.id == "bottom").Value);
                stage.BottomGradientColor = Color.FromArgb(bottom.A, bottom.R, bottom.G, bottom.B);
                var top = System.Drawing.ColorTranslator.FromHtml("#" + gradient.str.Single(x => x.id == "top").Value);
                stage.TopGradientColor = Color.FromArgb(top.A, top.R, top.G, top.B);
            }

            return stage;
        }
    }
}