using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Forms;

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

