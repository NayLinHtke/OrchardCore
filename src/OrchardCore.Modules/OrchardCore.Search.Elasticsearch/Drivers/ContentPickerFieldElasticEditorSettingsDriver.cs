using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Search.Elasticsearch.Core.Models;
using OrchardCore.Search.Elasticsearch.Core.Services;

namespace OrchardCore.Search.Elasticsearch.Drivers
{
    public class ContentPickerFieldElasticEditorSettingsDriver : ContentPartFieldDefinitionDisplayDriver
    {
        private readonly ElasticIndexSettingsService _elasticIndexSettingsService;

        public ContentPickerFieldElasticEditorSettingsDriver(ElasticIndexSettingsService elasticIndexSettingsService)
        {
            _elasticIndexSettingsService = elasticIndexSettingsService;
        }

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
        {
            return Initialize<ContentPickerFieldElasticEditorSettings>("ContentPickerFieldElasticEditorSettings_Edit", async model =>
            {
                partFieldDefinition.PopulateSettings(model);
                model.Indices = (await _elasticIndexSettingsService.GetSettingsAsync()).Select(x => x.IndexName).ToArray();
            }).Location("Editor");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            if (partFieldDefinition.Editor() == "Elasticsearch")
            {
                var model = new ContentPickerFieldElasticEditorSettings();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                context.Builder.WithSettings(model);
            }

            return Edit(partFieldDefinition);
        }

        public override bool CanHandleModel(ContentPartFieldDefinition model)
        {
            return string.Equals("ContentPickerField", model.FieldDefinition.Name);
        }
    }
}
