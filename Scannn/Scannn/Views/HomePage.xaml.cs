using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scannn
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            // A custom renderer is used to display the home UI
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
        }
    }
}