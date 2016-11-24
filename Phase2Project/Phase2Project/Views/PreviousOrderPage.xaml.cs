using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Phase2Project
{
    
    public partial class PreviousOrderPage : ContentPage
    {
        List<EmotionModel> emotionList = new List<EmotionModel> { };
        List<string> dateList = new List<string> { };
        public PreviousOrderPage()
        {
            InitializeComponent();
            
            ShowPreviousOrder();

           
        }
        private async void ShowPreviousOrder()
        {
            emotionList = await AzureManager.AzureManagerInstance.GetEmotionModelTimelines();
            foreach (EmotionModel model in emotionList)
            {
                dateList.Add(model.updateTime);
            }
            OrderListView.ItemsSource = dateList;
        }
    }
}
