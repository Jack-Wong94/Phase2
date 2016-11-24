using System;
using System.Linq;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Microsoft.ProjectOxford.Emotion;

//The main menu of the app including get previous record and order food
//Author: Long-Sing Wong
namespace Phase2Project
{
    public partial class MainPage : ContentPage
    {
        FaceBookModel profile;
        Button button;
        Button button2;

        //basic layout of the page
        public MainPage(FaceBookModel profile)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            this.profile = profile;
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;
            var label = new Label { Text = "Welcome "+profile.Name, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            button = new Button { Text = "Take a photo", BackgroundColor =Color.Silver, TextColor = Color.White, BorderRadius = 0};
            button2 = new Button { Text = "Get the past order", BackgroundColor = Color.Silver, TextColor = Color.White, BorderRadius = 0 };
            button.Clicked += TakePicture_Clicked;
            button2.Clicked += ChangeToPreviousOrderPage;
            layout.Children.Add(label);
            layout.Children.Add(button);
            layout.Children.Add(button2);

        }
        //Change page to get the previous order of the user.
        private async void ChangeToPreviousOrderPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PreviousOrderPage(profile));
        }
        //Change page after photo has been taken
        private async void ChangeToFoodPage(EmotionModel _emotionModel)
        {
            await Navigation.PushAsync(new FoodPage(_emotionModel,profile));
        }
        //Use the camera to take the picture and call microsoft cognitive service emotion api to find out the emotion of user
        private async void TakePicture_Clicked(object sender, System.EventArgs e)
        {
            //Check permission of using camera
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }
            //successfully taken the photo
            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                button.IsEnabled = false;
                button2.IsEnabled = false;
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    Directory = "Phase2",
                    Name = $"{DateTime.UtcNow}.jpg",
                    CompressionQuality = 92

                });

                //if photo has not been taken, enable the buttons
                if (file == null)
                {
                    button.IsEnabled = true;
                    button2.IsEnabled = true;
                    return;
                }
                
                //use the api key to call the external api to find out the emotion    
                try
                {
                    
                    string emotionKey = "ff39772d2a764944a93eee520503eb4b";
                    EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);

                    //get the unprocessed result.
                    var emotionResults = await emotionClient.RecognizeAsync(file.GetStream());
                    var temp = emotionResults[0].Scores;

                    //store the result emotion of the client
                    var clientEmotion = temp.ToRankedList().First().Key;

                    //build the emotion model using the user facebook id and their emotion and the time it is taken.
                    EmotionModel model = new EmotionModel()
                    {
                        Name = profile.Name,
                        Emotion = clientEmotion,
                        updateTime = DateTime.Now.ToString(),
                        facebookId = profile.id
                    };
                    
                    //change to food page to confirm if the client wants to order the dish.
                    ChangeToFoodPage(model);
                    button.IsEnabled = true;
                    button2.IsEnabled = true;
                }

                catch (Exception ex)
                {
                    errorLabel.Text = ex.Message;
                }

            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                
            }

        }
    }
}