using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Newtonsoft.Json;

//The first page of the app. Implement facebook login authorization.
//Author: Long-Sing Wong
namespace Phase2Project
{
    public partial class HomePage : ContentPage
    {
        //the client id for the facebook login api.
        private string ClientID = "1123044521077441";
        //facebook model to store all the info of the client including name, facebookid.
        private FaceBookModel profile;

        public HomePage()
        {
            InitializeComponent();
            //Layout of the page
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;
            var label = new Label
            {   Text = "Welcome to Fabrikam food! We have invented different crusine that fits your emotions. Simply take a selfie and order the dishes",
                TextColor = Color.Black,
                FontSize = 20

            };

            //facebook button binding with login method
            var button = new Button
            {
                Text = "Login using Facebook",
                BackgroundColor = Color.Silver,
                VerticalOptions = LayoutOptions.End
            };
            button.Clicked += Login;
            layout.Children.Add(label);
            layout.Children.Add(button);
        }

        //method to login using facebook account
        private void Login(object sender, System.EventArgs e)
        {
            //
            var apiRequest = "https://www.facebook.com/dialog/oauth?client_id="
                + ClientID
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";
            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };
            webView.Navigated += WebViewOnNavigated;
            Content = webView;

        }
        //this method use the access token to get the user profile
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {

                profile = await GetFacebookProfileAsync(accessToken);
                
                ChangePage();

            }
        }
        //Once login success, it change to main page
        private async void ChangePage()
        {
            await Navigation.PushAsync(new MainPage(profile));
        }

        //get the facebook profile and deserialize the json response to build the user profile
        public async Task<FaceBookModel> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.7/me/"
                + "?fields=name,picture,cover,age_range,devices,email,gender,is_verified"
                + "&access_token=" + accessToken;
            var httpClient = new HttpClient();
            var userJson = await httpClient.GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FaceBookModel>(userJson);
            var name = facebookProfile.Name;
            var id = facebookProfile.id;
            var email = facebookProfile.email;
            var gender = facebookProfile.gender;

            //build the user profile
            FaceBookModel model = new FaceBookModel()
            {
                Name = name,
                id = id,
                email = email,
                gender = gender
                

            };

            //send the data back to the FaceBookModel easy table. Each entry contains one user profile.
            try
            {
                await AzureManager.AzureManagerInstance.AddTimeline(model);
            }
            catch (Microsoft.WindowsAzure.MobileServices.MobileServiceConflictException) { }

            return facebookProfile;
        }

        //get the access token from the url
        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in"))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }
                var accessToken = at.Remove(at.IndexOf("&expires_in="));
                return accessToken;
            }
            return string.Empty;
        }
    }
}

