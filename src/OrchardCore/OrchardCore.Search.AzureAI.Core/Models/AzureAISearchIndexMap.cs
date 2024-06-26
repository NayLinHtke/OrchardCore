using System;
using OrchardCore.Indexing;
using static OrchardCore.Indexing.DocumentIndex;

namespace OrchardCore.Search.AzureAI.Models;

public class AzureAISearchIndexMap
{
    public string IndexingKey { get; set; }

    public string AzureFieldKey { get; set; }

    public Types Type { get; set; }

    public DocumentIndexOptions Options { get; set; }

    public AzureAISearchIndexMap()
    {

    }

    public AzureAISearchIndexMap(string azureFieldKey, Types type)
    {
        ArgumentException.ThrowIfNullOrEmpty(azureFieldKey, nameof(azureFieldKey));

        AzureFieldKey = azureFieldKey;
        Type = type;
    }

    public AzureAISearchIndexMap(string azureFieldKey, Types type, DocumentIndexOptions options)
        : this(azureFieldKey, type)
    {
        Options = options;
    }
}
