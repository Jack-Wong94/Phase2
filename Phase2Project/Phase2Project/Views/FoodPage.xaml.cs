﻿using Xamarin.Forms;

//This Page is used to confirm if the client wants to order the dishes
//Author: Long-Sing Wong
namespace Phase2Project
{
    public partial class FoodPage : ContentPage
    {
        EmotionModel _emotionModel;
        FaceBookModel profile;
        Button button;
        Label label;

        //basic layout of the page
        public FoodPage(EmotionModel _emotionModel,FaceBookModel profile)
        {
            
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            this._emotionModel = _emotionModel;
            this.profile = profile;
            var image = new Image { Aspect = Aspect.AspectFit };
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            
            //preview of the dishes based on the emotion
            if (_emotionModel.Emotion.ToLower() == "happiness")
            {
                
                image.Source = ImageSource.FromFile("happyfood.jpg");
                label = new Label { Text = "You look happy. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };


            }
            else if(_emotionModel.Emotion.ToLower() == "anger")
            {
               
                image.Source = ImageSource.FromFile("angryfood.jpg");
                label = new Label { Text = "You look angry. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            }
            else if (_emotionModel.Emotion.ToLower() == "contempt")
            {
                
                image.Source = ImageSource.FromFile("contemptfood.jpg");
                label = new Label { Text = "You look contempt. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };

            }
            else if (_emotionModel.Emotion.ToLower() == "disgust")
            {
                
                image.Source = ImageSource.FromFile("disgustfood.jpg");
                label = new Label { Text = "You look disgust. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };

            }
            else if (_emotionModel.Emotion.ToLower() == "fear")
            {
                
                image.Source = ImageSource.FromFile("fearfood.jpg");
                label = new Label { Text = "You look fear. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            }
            else if (_emotionModel.Emotion.ToLower() == "neutral")
            {
                
                image.Source = ImageSource.FromFile("neutralfood.jpg");
                label = new Label { Text = "You look alright. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            }
            else if (_emotionModel.Emotion.ToLower() == "sadness")
            {
               
                image.Source = ImageSource.FromFile("sadnessfood.jpg");
                label = new Label { Text = "You look sad. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            }
            else if (_emotionModel.Emotion.ToLower() == "surprise")
            {
                
                image.Source = ImageSource.FromFile("surprisefood.jpg");
                label = new Label { Text = "You look surprised. Do you want it?", TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            }
            this.Content = layout;
            button = new Button { Text = "Order the food", BackgroundColor = Color.Silver, TextColor = Color.White, BorderRadius = 0 };
            button.Clicked += SendOrder;
            layout.Children.Add(image);
            
            layout.Children.Add(label);
            layout.Children.Add(button);


        }

        //The method to send the order to the EmotionModel easy table to store the order. Using HTTP PUT method
        private async void SendOrder(object sender, System.EventArgs e)
        {
            try
            {
                button.IsEnabled = false;
                await AzureManager.AzureManagerInstance.AddTimeLine(_emotionModel);
                ChangeToMainPage();
                
            }
            catch (Microsoft.WindowsAzure.MobileServices.MobileServiceConflictException) { }
        }

        //Once order has been sent, change it back to main page.
        private async void ChangeToMainPage()
        {
            await Navigation.PushAsync(new MainPage(profile));
        }
    }
}
