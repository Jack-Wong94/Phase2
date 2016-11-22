using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Phase2Project;

namespace Phase2Project
{
    public partial class MainPage : ContentPage
    {
        public MainPage(FaceBookModel profile)
        {
            InitializeComponent();

            var name = profile.Name;
        }
    }
}
