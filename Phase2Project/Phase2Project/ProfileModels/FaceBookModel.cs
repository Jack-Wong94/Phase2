using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Phase2Project
{
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
