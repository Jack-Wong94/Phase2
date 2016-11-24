using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Phase2Project;
using Microsoft.ProjectOxford.Emotion;
using Newtonsoft.Json;

namespace Phase2Project
{
    public partial class MainPage : ContentPage
    {
        FaceBookModel profile;
        public MainPage(FaceBookModel profile)
        {
            InitializeComponent();
            this.profile = profile;
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;
            var label = new Label { Text = "Welcome "+profile.Name, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            var button = new Button { Text = "Take a photo", BackgroundColor =Color.FromHex("A6E55E"), TextColor = Color.White, BorderRadius = 0};
            button.Clicked += TakePicture_Clicked;
            layout.Children.Add(label);
            layout.Children.Add(button);

        }
        private async void ChangePage()
        {
            await Navigation.PushAsync(new MainPage(profile));
        }
        private async void TakePicture_Clicked(object sender, System.EventArgs e)
        {
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }
            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                    Directory = "Phase2",
                    Name = $"{DateTime.UtcNow}.jpg",
                    CompressionQuality = 92

                });

                if (file == null)
                    return;
                try
                {

                    string emotionKey = "ff39772d2a764944a93eee520503eb4b";

                    EmotionServiceClient emotionClient = new EmotionServiceClient(emotionKey);

                    var emotionResults = await emotionClient.RecognizeAsync(file.GetStream());
                    

                    UploadingIndicator.IsRunning = false;

                    var temp = emotionResults[0].Scores;
                    
                    //EmotionView.ItemsSource = temp.ToRankedList();
                    var clientEmotion = temp.ToRankedList().First().Key;

                    EmotionModel emo = new EmotionModel()
                    {
                        Name = profile.Name,
                        Emotion = clientEmotion,
                        updateTime = DateTime.Now.ToString()
                    };
                    try
                    {
                        await AzureManager.AzureManagerInstance.AddTimeLine(emo);
                    }
                    catch (Microsoft.WindowsAzure.MobileServices.MobileServiceConflictException) { }
                    /*image.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });*/
                }
                catch (Exception ex)
                {
                    errorLabel.Text = ex.Message;
                }

            }
            else
            {
                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }

        }
    }
}