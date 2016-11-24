using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phase2Project;
using Xamarin.Forms;

namespace Phase2Project
{
    public partial class FoodPage : ContentPage
    {
        EmotionModel _emotionModel;
        FaceBookModel profile;
        Button button;
        public FoodPage(EmotionModel _emotionModel,FaceBookModel profile)
        {
            
            InitializeComponent();
            this._emotionModel = _emotionModel;
            this.profile = profile;
            var image = new Image { Aspect = Aspect.AspectFit };
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            if (_emotionModel.Emotion.ToLower() == "happiness")
            {
                
                image.Source = ImageSource.FromFile("happyfood.jpg");
                
                

            }else if(_emotionModel.Emotion.ToLower() == "anger")
            {
               
                image.Source = ImageSource.FromFile("angryfood.jpg");
                
            }
            else if (_emotionModel.Emotion.ToLower() == "contempt")
            {
                
                image.Source = ImageSource.FromFile("contemptfood.jpg");
                
            }
            else if (_emotionModel.Emotion.ToLower() == "disgust")
            {
                
                image.Source = ImageSource.FromFile("disgustfood.jpg");
                
            }
            else if (_emotionModel.Emotion.ToLower() == "fear")
            {
                
                image.Source = ImageSource.FromFile("fearfood.jpg");
                
            }
            else if (_emotionModel.Emotion.ToLower() == "neutral")
            {
                
                image.Source = ImageSource.FromFile("neutralfood.jpg");
               
            }
            else if (_emotionModel.Emotion.ToLower() == "sadness")
            {
               
                image.Source = ImageSource.FromFile("sadnessfood.jpg");
                
            }
            else if (_emotionModel.Emotion.ToLower() == "surprise")
            {
                
                image.Source = ImageSource.FromFile("surprisefood.jpg");
                
            }
            this.Content = layout;
            button = new Button { Text = "Order the food", BackgroundColor = Color.Silver, TextColor = Color.White, BorderRadius = 0 };
            button.Clicked += SendOrder;
            layout.Children.Add(image);
            layout.Children.Add(button);


        }
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
        private async void ChangeToMainPage()
        {
            await Navigation.PushAsync(new MainPage(profile));
        }
    }
}
