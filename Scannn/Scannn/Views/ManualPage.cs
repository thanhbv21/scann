using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Scannn.Views
{
    public class ManualPage : ContentPage
    {
        public ManualPage()
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
                    new Label {
                        Margin = new Thickness(0, 20 , 0, 0),
                        Text = "- BƯỚC 1: TẢI ỨNG DỤNG",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                    new Label {
                        Text = "- BƯỚC 2: TẠO TÀI KHOẢN",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                    new Label {
                        Text = "- BƯỚC 3: QUÉT QRCODE",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                    new Label {
                        Text = "\t+ Bấm vào nút quét QRCode ở bên phải màn hình\n" +
                        "\t+ Đưa tem QRCode cần quét vào đúng khung quét",
                        TextColor = Color.Black
                    },
                    new Label {
                        Text = "- BƯỚC 4: XÁC NHẬN MUA (nếu có)",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                }
            };
        }
    }
}