using Azure.Search.Documents.Indexes;
using System.Text.Json.Serialization;



namespace EIdConsoleApp
{
    public class Product
    {
        [JsonPropertyName("product_id")]
        [SimpleField(IsKey = true, IsFilterable = true, IsSortable = true)]
        public string ProductId { get; set; }

        [JsonPropertyName("product_name")]
        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string ProductName { get; set; }

        [JsonPropertyName("description")]
        [SearchableField(IsFilterable = true, IsSortable = true)]
        public string ProductDescription { get; set; }


    }
}
