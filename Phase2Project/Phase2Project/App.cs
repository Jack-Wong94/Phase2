using Xamarin.Forms;

//app logic
//Author: Long-Sing Wong
namespace Phase2Project
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application

            var content = new HomePage
            {
                Title = "Emotional Food"

            };

            MainPage = new NavigationPage(content);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

