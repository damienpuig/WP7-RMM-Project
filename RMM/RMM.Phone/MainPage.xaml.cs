using Microsoft.Phone.Controls;
using RMM.Phone.ViewModel;
using System.Linq;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;


namespace RMM.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string ACCOUNT = "account";
        private const string CATEGORY = "category";

        private MainViewModel ViewModel
        {
            get
            {
                return DataContext as MainViewModel;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if( NavigationContext.QueryString.Values.Count != 0 )
                {
                    if (NavigationContext.QueryString.Values.First() == ACCOUNT)
                    {
                        ViewModel.RefreshAccountAndFavori();
                    }
                    else if (NavigationContext.QueryString.Values.First() == CATEGORY)
                    {
                        ViewModel.RefreshCategory();
                    }
                }
            });
            base.OnNavigatedTo(e);
        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void appbar_button1_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/About.xaml",System.UriKind.Relative));
        }

        private void addCategoryMenuItem_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/CreateCategory.xaml", System.UriKind.Relative));
        }

        private void addAccountMenuItem_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/CreateAccount.xaml", System.UriKind.Relative));
        }

        private void appbar_button1_Click_1(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/LiveSignIn.xaml", System.UriKind.Relative));
        }
    }
}
