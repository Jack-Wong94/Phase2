using Newtonsoft.Json;

//EmotionModel contains the information about user name and facebookId.
//The primary key of the table is id but using a foreign key which is facebookId from FaceBookModel.
//Using foreign key helps to retrieve data from the database since facebookId is unique.
//Author: Long-Sing Wong
namespace Phase2Project
{
    public class EmotionModel
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Emotion")]
        public string Emotion { get; set; }

        [JsonProperty(PropertyName = "UpDateTime")]
        public string updateTime { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "FaceBookId")]
        public string facebookId { get; set; }
    }
}
