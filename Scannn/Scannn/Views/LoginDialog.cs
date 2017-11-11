using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Scannn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scannn.Views
{
    public partial class LoginDialog : PopupPage
    {
        Entry user_email, user_password;
        Button LoginButton;
        Label notiLabel;
        public LoginDialog()
        {
            //InitializeComponent();
            Label LoginLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "ĐĂNG NHẬP"
            };
            notiLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Red,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                IsVisible = false,
                Text = "Lỗi đăng nhập"
            };
            user_email = new Entry()
            {
                WidthRequest = Application.Current.MainPage.Width - 60,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)),
                Placeholder = "Email"
            };

            user_password = new Entry()
            {
                WidthRequest = Application.Current.MainPage.Width - 60,
                IsPassword = true,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry)),
                Placeholder = "Password"
            };

            LoginButton = new Button
            {
                Text = "Đăng nhập",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex(Constants.ColorPrimary),
                IsVisible = true,
                
            };
            LoginButton.Clicked += LoginClicked;
            Content = new StackLayout()
            {
                BackgroundColor = Color.White,
                Children =
                {
                    LoginLabel,
                    notiLabel,
                    user_email,
                    user_password,
                    LoginButton
                },
                VerticalOptions = LayoutOptions.Center,
            };
            //this.BackgroundColor = new Color(0, 0, 0, 0.2);
            this.HasSystemPadding = true;
            this.Padding = new Thickness(20, 0, 20, 0);
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(user_email.Text + " " + password.Text);
            try
            {
                //System.Diagnostics.Debug.WriteLine(user_email.Text + " " + password.Text);
                LoginButton.IsEnabled = false;
                notiLabel.IsVisible = false;
                var email = user_email.Text;
                var password = user_password.Text;
                LoginAPI LA = await App.SvLoginManager.DoLoginAsync(email, password);
                if (LA.code != "200")
                {
                    notiLabel.Text = LA.desc;
                    notiLabel.IsVisible = true;
                }
                else
                {
                    var user = new UserProfile();
                    user.id = "1";
                    
                    user.email = email;
                    user.sessionid = LA.session;
                    App.sessionId = LA.session;
                    user.fullname = await App.SvLoginManager.GetFullNameWitoutSSAsync(email);
                    await App.UDatabase.SaveItemAsync(user);
                    App.fullname = user.fullname;
                    App.email = email;
                    await PopupNavigation.PopAsync();
                }
                LoginButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override bool OnBackgroundClicked()
        {
            System.Diagnostics.Debug.WriteLine("backgroundclicked");
            // Return default value - CloseWhenBackgroundIsClicked
            return base.CloseWhenBackgroundIsClicked;
        }
    }
}
