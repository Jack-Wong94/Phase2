using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

//The azuremanager manages the data transaction between the app and the database
//Author: Long-Sing Wong
namespace Phase2Project
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<FaceBookModel> profileTable;
        private IMobileServiceTable<EmotionModel> emotionTable;

        //Initialize the azure manager
        private AzureManager()
        {
            //initialize the client uri. The uri could be obtained from the azure account.
            this.client = new MobileServiceClient("http://phase2app.azurewebsites.net");

            //There are two tables for this app: FaceBookModel and EmotionModel
            this.profileTable = this.client.GetTable<FaceBookModel>();
            this.emotionTable = this.client.GetTable<EmotionModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }
        //return an instance of azure manager
        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }
                return instance;
            }
        }

        //The following methods are used for HTTP transport protocol
        //HTTP PUT method
        public async Task AddTimeline(FaceBookModel timeline)
        {
            await this.profileTable.InsertAsync(timeline);
        }
        public async Task AddTimeLine(EmotionModel timeline)
        {
            await emotionTable.InsertAsync(timeline);
        }
        //HTTP GET method
        public async Task<List<FaceBookModel>> GetProfileTimelines()
        {
            return await this.profileTable.ToListAsync();
        }
        //HTTP GET method using query to get all the emotion data if facebookId is the same as the client id.
        public async Task<List<EmotionModel>> GetEmotionModelTimelines(string id)
        {
            return await emotionTable.Where(model => model.facebookId == id).ToListAsync();
        }
        //HTTP UPDATE method
        public async Task UpdateTimeline(FaceBookModel profile)
        {
            await this.profileTable.UpdateAsync(profile);
        }
    }
}

