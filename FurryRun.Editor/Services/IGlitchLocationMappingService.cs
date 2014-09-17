using System.Collections.Generic;
using FurryRun.Editor.Model;

namespace FurryRun.Editor.Services
{
    public interface IGlitchLocationMappingService
    {
        Stage MapGlitchLocationFileToModel(game_object gameObject);
        SortedList<int, Layer> MapGlitchLayers(@object layerObj);
        IList<Filter> MapGlitchFilters(@object single);
        Stage MapGlitchObjectToStage(@object obj);
        SortedList<int, LayerItem> MapGlitchItems(@object decos);
    }
}