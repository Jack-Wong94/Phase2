using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Phase2Project
{
    public class EmotionModel
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Emotion")]
        public string Emotion { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }
}
