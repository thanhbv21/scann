using Rg.Plugins.Popup.Extensions;
using Scannn.Data;
using Scannn.Models;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;

namespace Scannn.Views
{
    public class ResultPage : ContentPage
    {
        public string color_theme = Constants.ColorPrimary;
        private bool disback = true;
        
        public ResultPage(string itemcode)
        {
            App.Bought = true;
            App.soldtime = null;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            this.Title = "Thông tin sản phẩm";
            Debug.WriteLine("Khởi tạo ban đầu");
            CheckLoadingInit(itemcode);
        }

        private void CheckLoadingInit(string itemcode)
        {
            if (DependencyService.Get<ICheckInternet>().IsInternet())
            {
                
                NavigationPage.SetHasNavigationBar(this, false);
                Debug.WriteLine("Mạng Ok");
                var layout = new StackLayout();
                var indicator = new ActivityIndicator()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Color = Color.Black,
                    IsEnabled = true,
                    IsRunning = true
                };
                var label = new Label()
                {
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Text = "Đang tải dữ liệu..."
                };
                layout.Children.Add(indicator);
                layout.Children.Add(label);
                Content = layout;
                Debug.WriteLine(itemcode[0]);
                if (itemcode[0].ToString().Equals("B")) SetWebLayout(itemcode, 2);
                else
                {
                    if (itemcode.Length == 13) SetLayout(itemcode);
                    else SetWebLayout(itemcode, 1);
                }
            }
            else
            {
                Debug.WriteLine("Ko có mạng");
                Content = SetNoNetworkView(itemcode);
            }
        }

        private void SetWebLayout(string itemcode, int mode)
        {
            //1 là mode ezcheck cũ, 2 là lời hay ý đẹp
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, true);
            StackLayout stackweb = new StackLayout();
            /*
            ProgressBar probar = new ProgressBar()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false,
                Progress = 0.2,
            };
            resultweb.Navigating += (sender, e) =>
            {
                probar.IsVisible = true;
            };
            resultweb.Navigated += (sender, e) =>
            {
                probar.IsVisible = false;
            };
            */
            var resultweb = new WebView
            {
                HeightRequest = App.Current.MainPage.Height,
                WidthRequest = App.Current.MainPage.Width,
            };
            
            if (mode == 2)
            {
                resultweb.Source = string.Format(Constants.WebLHYDUrl, itemcode);
                this.Title = "Lời hay ý đẹp";
            }
            else 
                resultweb.Source = string.Format(Constants.WebUrl, itemcode);

            //stackweb.Children.Add(probar);
            stackweb.Children.Add(resultweb);
            Content = stackweb;
        }

        private StackLayout SetNoNetworkView(string itemcode)
        {
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, true);
            var nonetstack = new StackLayout();
            var verifyimage = new Image
            {
                Margin = new Thickness(0, Application.Current.MainPage.Height / 3, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = Application.Current.MainPage.Width / 2,
                WidthRequest = HeightRequest,
                Source = "no_internet_connection"
            };

            
            var status = new Label()
            {
                Text = "Xin lỗi, hiện không có kết nối Internet",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var reminder = new Label()
            {
                Text = "Xin hãy bật kết nối mạng và thử lại",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var retrybutton = new Button()
            {
                Margin = new Thickness(Application.Current.MainPage.Width / 4, 0, Application.Current.MainPage.Width / 4, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.FromHex(color_theme),
                Text = "Thử lại",
                TextColor = Color.White
            };
            nonetstack.Children.Add(verifyimage);
            nonetstack.Children.Add(status);
            nonetstack.Children.Add(reminder);
            nonetstack.Children.Add(retrybutton);

            retrybutton.Clicked += delegate
            {
                CheckLoadingInit(itemcode);
            };

            return nonetstack;
        }

        public async void SetLayout(string itemcode)
        {
            Debug.WriteLine("active layout");
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetHasNavigationBar(this, true);
            string hotline = "19001122";

            var scroll = new ScrollView()
            {
                BackgroundColor = Color.FromHex("#dddee3")
            };
            var layout = new StackLayout();
            var paddingTLR = new Thickness(10, 10, 10, 10);
            var paddingTB = new Thickness(0, 10, 0, 10);
            
            //Trích xuất dữ liệu
            LayoutDataResult dataresult = new LayoutDataResult();
            ProductAPI proapi = new ProductAPI();
            CompanyAPI comapi = new CompanyAPI();
            ImageAPI image = new ImageAPI();
            try
            {
                proapi = await App.SvManager.GetProductAsync(itemcode);
                comapi = await App.SvManager.GetCompanyAsync(itemcode);
                image = await App.SvManager.GetImageAsync(itemcode);
                Product pro = proapi.item;
                Company com = comapi.company;

                dataresult.product = pro;
                dataresult.company = com;

                if (image.image.Contains("data:image/png;base64,"))
                {
                    string head = "data:image/png;base64,";
                    image.image = image.image.Remove(0, head.Length);
                }
                Debug.WriteLine(dataresult.image);

                dataresult.image = image.image;
                StackLayout stack1 = new StackLayout();
                Image verifyimage = new Image
                {
                    HeightRequest = Application.Current.MainPage.Width / 4,
                    WidthRequest = Application.Current.MainPage.Width / 4,
                };
                System.Diagnostics.Debug.WriteLine("Image point" + Application.Current.MainPage.Width / 4);
                Label verifyLabel = new Label()
                {
                    TextColor = Color.Red
                };
                Label thanksLabel = new Label()
                {
                    TextColor = Color.Red
                };
                var muaspButton = new Button
                {
                    Text = "MUA SẢN PHẨM",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex(Constants.ColorPrimary),
                    IsVisible = App.Bought
                };
                string verifyLabelText = "", thanksLabelText = "";
                stack1.Children.Add(verifyimage);
                stack1.Children.Add(verifyLabel);
                stack1.Children.Add(thanksLabel);
                layout.Children.Add(stack1);

                stack1.BackgroundColor = Color.White;
                stack1.Padding = paddingTB;
                layout.Spacing = 10;
                scroll.Content = layout;

                
                if (proapi.code == 444 || proapi.code == 400 || proapi.code == 445)
                {
                    verifyimage.Source = "icon_result_2";
                    verifyLabelText = "SẢN PHẨM KHÔNG CÓ TRONG HỆ THỐNG";
                    thanksLabelText = "Bạn hãy cẩn thận";
                    verifyLabel.Text = verifyLabelText;
                    verifyLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    verifyLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

                    thanksLabel.Text = thanksLabelText;
                    thanksLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    thanksLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
                }
                else
                {

                    if (dataresult.product.ite_status == "3")
                    {
                        verifyimage.Source = "icon_result_3";
                        verifyLabelText = "SẢN PHẨM CHÍNH HÃNG";
                        thanksLabelText = "Cảm ơn bạn đã mua hàng";
                        verifyLabel.TextColor = Color.Green;
                        thanksLabel.TextColor = Color.Green;
                        App.Bought = true;
                    }
                    else
                    if (dataresult.product.ite_status != "3" && proapi.changed == 0)
                    {
                        verifyimage.Source = "icon_result_1";
                        verifyLabelText = "SẢN PHẨM ĐÃ ĐƯỢC MUA";
                        thanksLabelText = "Ngày bán: " + pro.ite_soldtime;
                        App.Bought = false;
                    }
                   
                    else
                    if (dataresult.product.ite_status != "3" && proapi.changed == 1)
                    {
                        verifyimage.Source = "icon_result_1";
                        verifyLabelText = "XIN CẢM ƠN BẠN ĐÃ MUA HÀNG";
                        thanksLabelText = "Vào lúc: " + pro.ite_soldtime;
                        verifyLabel.TextColor = Color.Green;
                        thanksLabel.TextColor = Color.Green;
                        App.Bought = true;
                    }
                    Debug.WriteLine(verifyimage.Source.ToString());
                    verifyLabel.Text = verifyLabelText;
                    verifyLabel.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    verifyLabel.FontAttributes = FontAttributes.Bold;
                    verifyLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

                    thanksLabel.Text = thanksLabelText;
                    thanksLabel.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    thanksLabel.HorizontalOptions = LayoutOptions.CenterAndExpand;

                    //Ảnh sản phẩm
                    var proimage = new Image()
                    {
                        WidthRequest = App.Current.MainPage.Width / 3
                    };
                    if (dataresult.image != "")
                    {
                        proimage = new Image();
                        try
                        {
                            byte[] proimageBytes = Convert.FromBase64String(dataresult.image);
                            proimage.Source = ImageSource.FromStream(
                                       () => new MemoryStream(proimageBytes));
                        }
                        catch
                        {
                            proimage.Source = "icon_default";
                        }
                        
                    }
                    else
                    {
                        proimage.Source = "icon_default";
                    }
                    //Nội dung sản phẩm
                    var productdetailStack = new StackLayout();
                    var itemnameLabel = new Label
                    {
                        Text = pro.pro_name,
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
                    stackitemcodeLabel.Children.Add(maSPLabel);
                    stackitemcodeLabel.Children.Add(itemcodeLabel);

                    productdetailStack.Children.Add(itemnameLabel);
                    productdetailStack.Children.Add(stackitemcodeLabel);
                    productdetailStack.Children.Add(hotlineLabel);
                    productdetailStack.Children.Add(muaspButton);

                    var grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition { Height = Application.Current.MainPage.Width/3 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    grid.Children.Add(proimage, 0, 0);
                    grid.Children.Add(productdetailStack, 1, 0);
                    var stack2 = new StackLayout();
                    stack2.Children.Add(grid);

                    //Thông tin chi tiết sản phẩm 
                    var stack3 = new StackLayout();
                    var productdetailWebView = new WebView
                    {
                        Source = new HtmlWebViewSource
                        {
                            Html = @"<html><body>" + dataresult.product.pro_detail + "</body></html>"
                        },
                        HeightRequest = 250,
                        //MinimumHeightRequest = 100,
                        WidthRequest = 1000
                    };
                    var productdetailtitleLabel = new Label
                    {
                        Text = "THÔNG TIN SẢN PHẨM",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black
                    };
                    stack3.Children.Add(productdetailtitleLabel);

                    var gapBoxView1 = new BoxView
                    {
                        Color = Color.Gray,
                        HeightRequest = 3,
                        WidthRequest = (Application.Current.MainPage.Width / 7) * 6,
                        VerticalOptions = LayoutOptions.Center
                    };
                    stack3.Children.Add(gapBoxView1);
                    stack3.Children.Add(productdetailWebView);

                    //Mô tả công ty
                    var stack4 = new StackLayout();
                    var companydetailview = new StackLayout();
                    var companytitleLabel = new Label
                    {
                        Text = "DOANH NGHIỆP SỞ HỮU",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companytitleLabel);
                    var gapBoxView = new BoxView
                    {
                        Color = Color.Gray,
                        HeightRequest = 3,
                        WidthRequest = (Application.Current.MainPage.Width / 7) * 6,
                        VerticalOptions = LayoutOptions.Center
                    };
                    companydetailview.Children.Add(gapBoxView);
                    var companynameLabel = new Label
                    {
                        Text = "Tên công ty: " + dataresult.company.name,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companynameLabel);

                    var companyaddressLabel = new Label
                    {
                        Text = "Địa chỉ: " + dataresult.company.addr,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companyaddressLabel);

                    var companyphoneLabel = new Label
                    {
                        Text = "Điện thoại: " + dataresult.company.mobile,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companyphoneLabel);

                    var companyemailLabel = new Label
                    {
                        Text = "Email: " + dataresult.company.email,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companyemailLabel);

                    var companywebsiteLabel = new Label
                    {
                        Text = "Website: " + dataresult.company.website,
                        TextColor = Color.Black
                    };
                    companydetailview.Children.Add(companywebsiteLabel);

                    stack4.Children.Add(companydetailview);
                    var endBoxView = new BoxView
                    {
                        BackgroundColor = Color.FromHex(color_theme),
                        HeightRequest = 50
                    };
                    stack2.Padding = paddingTB;
                    stack3.Padding = paddingTLR;
                    stack4.Padding = paddingTLR;

                    stack2.BackgroundColor = Color.White;
                    stack3.BackgroundColor = Color.White;
                    stack4.BackgroundColor = Color.White;

                    layout.Children.Add(stack2);
                    layout.Children.Add(stack3);
                    layout.Children.Add(stack4);
                    layout.Children.Add(endBoxView);

                    //Lưu dự liệu scan vào lịch sử
                    //DateTime now = DateTime.Now.ToLocalTime();
                    string datestring = DateTime.Now.ToString("dd/MM/yy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    Debug.WriteLine(datestring);
                    HistoryScanItem scannedItem = new HistoryScanItem()
                    {
                        itemcode = itemcode,
                        itemname = pro.pro_name,
                        itemimage = image.image,
                        companyname = com.name,
                        datetime = datestring
                    };

                    Debug.WriteLine("active database");
                    //App.HDatabase.DeleteAllAsync();
                    await App.HDatabase.SaveItemAsync(scannedItem);
                    App.AppHSI.Insert(0, scannedItem);//xử lý listview android
                    Debug.WriteLine("database save done!");
                    disback = false;
                    muaspButton.Clicked += async delegate (object sender, EventArgs e)
                    {
                        //var confirmgrid = grid;
                        await Navigation.PushPopupAsync(new BuyDialog(dataresult.image, itemcode, hotline, pro.pro_name));
                    };
                    //var locationresp;
                    Debug.WriteLine("active location");
                    var location = new LocationAPI();
                    Device.BeginInvokeOnMainThread(async () =>
                        {
                            location = await App.SvLocationManager.GetLocationAsync();
                            try
                            {
                                await App.SvLocationManager.UpdateLocationAsync(location, true, itemcode);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("ko lưu đc location " + ex);
                            }
                        }
                    );
                    Device.StartTimer(TimeSpan.FromSeconds(0.5), () =>
                    {
                        // Do something
                        //Debug.WriteLine("Chỉ số đã mua: " + App.Bought);
                        if (App.Bought == false) muaspButton.IsVisible = false;
                        //Nút mua sẽ luôn hiển thị khi giá trị bought là false (đã mua)
                        if (App.Bought == false && App.soldtime != null)
                        {
                            //giá trị text chỉ thực hiện thay đổi sau khi đã click vào nút mua tức giá trị soldtime sẽ thay đổi
                            //App soldtime sẽ được khai báo null khi tạo mới một resultpage và sẽ được gán giá trị mới khi click vào mua hàng
                            //verifyimage.Source = "icon_result_1";
                            verifyLabel.Text = "XIN CẢM ƠN BẠN ĐÃ MUA HÀNG";
                            thanksLabel.Text = "Ngày bán: " + App.soldtime;
                            return false;
                        }
                        return true; // True = Repeat again, False = Stop the timer
                    });

                }
                Content = scroll;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("Type" + ex.GetType());
                if (ex.GetType().ToString() == "System.NotImplementedException")
                {
                    CheckLoadingInit(itemcode);
                }
                Content = SetErrorPage();
            }


        }

        private StackLayout SetErrorPage()
        {
            NavigationPage.SetHasNavigationBar(this, true);
            var nonetstack = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var status = new Label()
            {
                Text = "ERROR! :(",
                FontSize = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            var reminder = new Label()
            {
                Text = "Đã có lỗi xảy ra, xin hãy quay lại và thử lại",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            nonetstack.Children.Add(status);
            nonetstack.Children.Add(reminder);

            return nonetstack;
        }

        protected override bool OnBackButtonPressed()
        {
            if (disback)
                return true;
            return base.OnBackButtonPressed(); ;
        }
        
    }
}
