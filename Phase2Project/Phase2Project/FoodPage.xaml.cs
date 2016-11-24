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
        public FoodPage(EmotionModel _emotionModel)
        {
            
            InitializeComponent();
            this._emotionModel = _emotionModel;
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
            layout.Children.Add(image);

        }
    }
}
