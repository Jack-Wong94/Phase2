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
        FaceBookModel profile;
        string Name;
        List<EmotionModel> emotionList = new List<EmotionModel> { };
        List<string> dateList = new List<string> { };
        public PreviousOrderPage(FaceBookModel profile)
        {
            InitializeComponent();
            this.profile = profile;
            Name = profile.Name;
            ShowPreviousOrder();

           
        }
        private async void ShowPreviousOrder()
        {
            emotionList = await AzureManager.AzureManagerInstance.GetEmotionModelTimelines(profile.id);

            foreach (EmotionModel model in emotionList)
            {
                dateList.Add(model.updateTime + "     "+"Order food of "+model.Emotion);
            }
            if (dateList != null)
            {
                OrderListView.ItemsSource = dateList;
            }else
            {
                OrderListView.ItemsSource = new List<string> { "No previous order." };
            }
            OrderListView.ItemSelected += OnSelection;
        }
        private void OnSelection(object sender,SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
        }
    }
}
