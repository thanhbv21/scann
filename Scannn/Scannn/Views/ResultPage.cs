using Scannn.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scannn.Views
{
    public class ResultPage : ContentPage 
    {
        public ResultPage()
        {
        }

        public async void setlayout(string itemcode)
        {
            Debug.WriteLine("active layout");
            //Debug.WriteLine(dataresult.product.pro_name);
            var scroll = new ScrollView();
            var layout = new StackLayout();
            IsBusy = true;
            layout.BackgroundColor = Color.White;
            /*
            var indicator = new ActivityIndicator()
            {
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                Color = Color.Black,
                IsEnabled = true

            };
            indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");
            indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            //layout.Children.Add(indicator);*/
            LayoutDataResult dataresult = new LayoutDataResult();

            Product pro = await App.SvManager.GetProductAsync(itemcode);
            Company com = await App.SvManager.GetCompanyAsync(itemcode);
            ImageAPI image = await App.SvManager.GetImageAsync(itemcode);

            //LayoutDataResult dataresult = new LayoutDataResult();
            dataresult.product = pro;
            dataresult.company = com;
            dataresult.image = image.image;

            IsBusy = false;
            //Ảnh kết quả scan
            var verifyimage = new Image();
            if (dataresult.product.ite_status == "3")
            {
                verifyimage = new Image
                {
                    //Scale = 0.5,
                    HeightRequest = 500,
                    WidthRequest = 500,
                    //Aspect = Aspect.AspectFit
                    Source = "icon_ket_qua_3"
                };
            }
            else
            {
                verifyimage = new Image
                {
                    HeightRequest = 500,
                    WidthRequest = 500,
                    //Aspect = Aspect.AspectFit,
                    Source = "icon_ket_qua_1"
                };
            }
            layout.Children.Add(verifyimage);

            //Ảnh sản phẩm
            byte[] proimageBytes = Convert.FromBase64String(dataresult.image);
            var proimage = new Image
            {
                Source = ImageSource.FromStream(
                       () => new MemoryStream(proimageBytes))
            };
            layout.Children.Add(proimage);


            //Nội dung sản phẩm
            var productdetailStack = new StackLayout();
            var itemcodeLabel = new Label
            {
                Text = "Mã Sản phẩm: " + itemcode
            };
            var productdetailWebView = new WebView
            {
                Source = new HtmlWebViewSource
                {
                    Html = @"<html><body>" + dataresult.product.pro_detail + "</body></html>"
                },
                HeightRequest = 100,
                WidthRequest = 1000
            };
            productdetailStack.Children.Add(itemcodeLabel);
            productdetailStack.Children.Add(productdetailWebView);

            //layout.Children.Add(productdetailStack);

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //var Left = new Label { Text = "Left" };
            //var Right = new Label { Text = "Right" };
            grid.Children.Add(proimage, 0, 0);
            grid.Children.Add(productdetailStack, 1, 0);
            layout.Children.Add(grid);

            //Mô tả công ty
            var companydetailview = new StackLayout();
            var companytitleLabel = new Label
            {
                Text = "Doanh nghiệp sở hữu"
            };
            companydetailview.Children.Add(companytitleLabel);

            var companynameLabel = new Label
            {
                Text = "Tên công ty: " + dataresult.company.com_name
            };
            companydetailview.Children.Add(companynameLabel);

            var companyaddressLabel = new Label
            {
                Text = "Địa chỉ: " + dataresult.company.com_addr
            };
            companydetailview.Children.Add(companyaddressLabel);

            var companyphoneLabel = new Label
            {
                Text = "Điện thoại: " + dataresult.company.com_mobile
            };
            companydetailview.Children.Add(companyphoneLabel);

            var companyemailLabel = new Label
            {
                Text = "Email: " + dataresult.company.com_email
            };
            companydetailview.Children.Add(companyemailLabel);

            var companywebsiteLabel = new Label
            {
                Text = "Website: " + dataresult.company.com_website
            };
            companydetailview.Children.Add(companywebsiteLabel);

            layout.Children.Add(companydetailview);
            var yellowBox = new BoxView
            {
                Color = Color.Yellow,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            layout.Children.Add(yellowBox);
            layout.Spacing = 10;
            scroll.Content = layout;
            Content = scroll;
        }
    }
}
