

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace RMM.Phone.ViewModel
{

    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AccountViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<EditAccountViewModel>();
            SimpleIoc.Default.Register<EditCategoryViewModel>();
            SimpleIoc.Default.Register<CreateAccountViewModel>();
            SimpleIoc.Default.Register<CreateCategoryViewModel>();
            SimpleIoc.Default.Register<LiveSignInViewModel>();
            SimpleIoc.Default.Register<RestaurerViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AccountViewModel Account
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountViewModel>();
            }
        }

        public CategoryViewModel Category
        {
            get 
            {
                return ServiceLocator.Current.GetInstance<CategoryViewModel>();
            }
        }

        public EditAccountViewModel EditAccount
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditAccountViewModel>();
            }

        }

        public EditCategoryViewModel EditCategory
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditCategoryViewModel>();
            }
        }

        public CreateAccountViewModel CreateAccount
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateAccountViewModel>();
            }
        }

        public CreateCategoryViewModel CreateCategory
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateCategoryViewModel>();
            }
        }

        public static LiveSignInViewModel Live
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LiveSignInViewModel>();
            }
        }

        public RestaurerViewModel Restaurer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RestaurerViewModel>();
            }
        }

        

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            //que c'est pas tres beau
            Live.Cleanup();
        }
    }
}