using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Scannn.Views
{
    public class TermsPage : ContentPage
    {
        public TermsPage()
        {
            BackgroundColor = Color.White;
            ScrollView scroll = new ScrollView();
            Content = scroll;
            var stack = new StackLayout
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
                        Text = "- ezCheck - Chứng minh thư cho từng sản phẩm",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Text = "+ Giúp người tiêu dùng:\n" +
                        "\t. Kiểm tra nguồn gốc xuất xứ của sản phẩm.\n" +
                        "\t. Xác nhận quyền sở hữu sản phẩm.\n" +
                        "\t. Bảo vệ người tiêu dùng khỏi hàng giả, hàng nhái.\n" +
                        "\t. Tra cứu thông tin và trao đổi trực tiếp với nhà sản xuất/nhà phân phối.\n" +
                        "\t. Cập nhật thông tin mới nhất về sản phẩm.\n" +
                        "+ Giúp nhà sản xuất/nhà phân phối:\n" +
                        "\t. Quảng cáo, giới thiệu sản phẩm tới khách hàng nhanh chóng.\n" +
                        "\t. Chống hàng giả, hàng nhái.\n" +
                        "\t. Tăng uy tín, thương hiệu.\n",
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Text = "- Miễn phí sử dụng.\n" +
                        "- Dễ dàng cài đặt và sử dụng.\n" +
                        "- Hỗ trợ các nền tảng: Android, iOS.\n" +
                        "\n" +
                        "- Để sử dụng đầy đủ tính năng, thiết bị cần có:",
                        FontSize = 14,
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Text =  "+ Camera hỗ trợ tính năng lấy nét.\n" +
                        "+ Kết nối Internet.",
                        TextColor = Color.Black
                    },
                    new Label
                    {
                        Text = "- ezCheck không có chức năng chứng thực chất lượng sản phẩm.",
                        FontSize = 14,
                        TextColor = Color.Black
                    }

                }
            };
            scroll.Content = stack;
        }
    }
}