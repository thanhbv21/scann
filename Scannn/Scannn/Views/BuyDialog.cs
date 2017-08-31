using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace Scannn.Views
{
    class BuyDialog : PopupPage
    {
        private string itemcode;

        public BuyDialog(string imagestring, string itemcode, string hotline, string proname )
        {
            this.itemcode = itemcode;
            Label confirmLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "XÁC NHẬN MUA SẢN PHẨM"
            };
            var proimage = new Image()
            {
                WidthRequest = App.Current.MainPage.Width / 3
            };
            if (imagestring != "")
            {

                byte[] proimageBytes = Convert.FromBase64String(imagestring);
                proimage = new Image
                {
                    Source = ImageSource.FromStream(
                           () => new MemoryStream(proimageBytes))
                };
            }
            else
            {
                proimage.Source = "icon_default";
            }
            var productdetailStack = new StackLayout();
            var itemnameLabel = new Label
            {
                Text = proname,
                TextColor = Color.Black
            };
            itemnameLabel.FontAttributes = FontAttributes.Bold;

            var stackitemcodeLabel = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };
            var maSPLabel = new Label
            {
                Text = "Mã SP: ",
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold
            };
            var itemcodeLabel = new Label
            {
                Text = itemcode,
                TextColor = Color.Black
            };
            var hotlineLabel = new Label
            {
                Text = "Hotline: " + hotline,
                TextColor = Color.Black
            };

            var muaspButton = new Button
            {
                Text = "MUA SẢN PHẨM",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex(Constants.ColorPrimary),
                IsVisible = true,
                
            };
            muaspButton.Clicked += MuaSpOnClicked;
            stackitemcodeLabel.Children.Add(maSPLabel);
            stackitemcodeLabel.Children.Add(itemcodeLabel);

            productdetailStack.Children.Add(itemnameLabel);
            productdetailStack.Children.Add(stackitemcodeLabel);
            productdetailStack.Children.Add(hotlineLabel);
            //productdetailStack.Children.Add(muaspButton);

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = Application.Current.MainPage.Width / 3 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.Children.Add(proimage, 0, 0);
            grid.Children.Add(productdetailStack, 1, 0);

            Content = new StackLayout()
            {
                BackgroundColor = Color.White,
                Children =
                    {
                    confirmLabel,
                    grid,
                    muaspButton
                },
                VerticalOptions = LayoutOptions.Center,
                //Padding = new Thickness(10, 0, 10, 0),
            };
            this.BackgroundColor = new Color(0, 0, 0, 0.4);
            this.Padding = new Thickness(10, 0, 10, 0);
            
        }

        private async void MuaSpOnClicked(object sender, EventArgs e)
        {
            App.Bought = false;
            //DateTime soldtime = DateTime.Now.ToLocalTime();
            await App.SvManager.PurchaseProductAsync(itemcode, true);
            App.soldtime = DateTime.Now.ToString("dd/MM/yy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            //System.Diagnostics.Debug.WriteLine(App.newResultPage.buy);
            await PopupNavigation.PopAsync();
        }
    }
}
