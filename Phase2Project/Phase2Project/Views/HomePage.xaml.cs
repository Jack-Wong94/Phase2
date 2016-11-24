using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Phase2Project;

namespace Phase2Project
{
    public partial class HomePage : ContentPage
    {
        private string ClientID = "1123044521077441";
        private FaceBookModel profile;
        public HomePage()
        {
            InitializeComponent();

        }
        private void Login(object sender, System.EventArgs e)
        {
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
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            var accessToken = ExtractAccessTokenFromUrl(e.Url);
            if (accessToken != "")
            {

                profile = await GetFacebookProfileAsync(accessToken);
                //MainPage main = new MainPage(profile);
                ChangePage();

            }
        }
        private async void ChangePage()
        {
            await Navigation.PushAsync(new MainPage(profile));
        }
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
            FaceBookModel emo = new FaceBookModel()
            {
                Name = name,
                id = id,
                email = email,
                gender = gender
                

            };
            try
            {
                await AzureManager.AzureManagerInstance.AddTimeline(emo);
            }
            catch (Microsoft.WindowsAzure.MobileServices.MobileServiceConflictException) { }

            return facebookProfile;
        }

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

