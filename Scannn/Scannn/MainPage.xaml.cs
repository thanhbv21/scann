using Scannn.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scannn
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var buttonScan = this.FindByName<Button>("BtnScan");
            buttonScan.Clicked += (sender, e) =>
            {
                DependencyService.Get<ActiveScan>().Scan();
            };
            
        }
        async void OnTakePhotoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultPage());
        }
    }
}
