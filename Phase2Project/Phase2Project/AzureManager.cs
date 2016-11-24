using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Phase2Project
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<FaceBookModel> profileTable;
        private IMobileServiceTable<EmotionModel> emotionTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://phase2app.azurewebsites.net");
            this.profileTable = this.client.GetTable<FaceBookModel>();
            this.emotionTable = this.client.GetTable<EmotionModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }
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
        public async Task AddTimeline(FaceBookModel timeline)
        {
            await this.profileTable.InsertAsync(timeline);
        }
        public async Task AddTimeLine(EmotionModel timeline)
        {
            await emotionTable.InsertAsync(timeline);
        }
        public async Task<List<FaceBookModel>> GetTimelines()
        {
            return await this.profileTable.ToListAsync();
        }
    }
}

