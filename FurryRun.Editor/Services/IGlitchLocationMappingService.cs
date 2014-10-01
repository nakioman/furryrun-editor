using System.Collections.Generic;
using FurryRun.Editor.Infrastructure;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public interface IGlitchLocationMappingService
    {
        Stage MapGlitchLocationFileToModel(game_object gameObject);
        SerializableSortedList<int, Layer> MapGlitchLayers(@object layerObj);
        List<Filter> MapGlitchFilters(@object single);
        Stage MapGlitchObjectToStage(@object obj);
        SerializableSortedList<int, LayerItem> MapGlitchItems(@object decos, Layer layer);
    }
}