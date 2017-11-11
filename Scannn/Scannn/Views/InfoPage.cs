using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Scannn.Views
{
    public class InfoPage : ContentPage
    {
        public InfoPage()
        {
            BackgroundColor = Color.White;
            Content = new StackLayout
            {
                Margin = new Thickness(20, 0, 20, 0),
                Children = {
                    new Image
                    {
                        Margin = new Thickness(0, 50 , 0, 0),
                        HorizontalOptions = LayoutOptions.Center,
                        HeightRequest = Application.Current.MainPage.Width / 4,
                        WidthRequest = HeightRequest,
                        Source = "ezcheck_Logo_v4_no_background"
                    },
                    new Label
                    {
                        Margin = new Thickness(0, 20 , 0, 0),
                        Text = "Phần mềm:\t\t\t\tezCheck",
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Margin = new Thickness(0, 10 , 0, 0),
                        Text = "Phiên bản:\t\t\t\t3.0.2",
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Margin = new Thickness(0, 10 , 0, 0),
                        Text = "Website:\t\t\t\twww.ezCheck.vn",
                        TextColor = Color.Black
                    }

                    }
            };


        }
    }
}