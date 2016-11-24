using System.Collections.Generic;

using Xamarin.Forms;

//Get the order made by the client previously.
//Author: Long-Sing Wong
namespace Phase2Project
{

    public partial class PreviousOrderPage : ContentPage
    {
        FaceBookModel profile;
        string Name;
        List<EmotionModel> emotionList = new List<EmotionModel> { };
        List<string> dateList = new List<string> { };

        //Show the list view of the order
        public PreviousOrderPage(FaceBookModel profile)
        {
            InitializeComponent();
            this.profile = profile;
            Name = profile.Name;
            ShowPreviousOrder();

           
        }
        //method to show the list based on the time it made.
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
        //When client select an item, pop up shows.
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
