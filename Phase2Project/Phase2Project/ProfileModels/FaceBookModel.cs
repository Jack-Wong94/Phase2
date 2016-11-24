using Newtonsoft.Json;

//The client profile model
//Author: Long-Sing Wong
namespace Phase2Project
{
    //attribute that should be included in the easy table
    public class FaceBookModel
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string email { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string gender { get; set; }
        
        
    }
}
