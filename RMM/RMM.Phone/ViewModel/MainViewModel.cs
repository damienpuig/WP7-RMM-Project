using GalaSoft.MvvmLight;
using RMM.Business.CategoryService;
using System.Collections.ObjectModel;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System;
using System.Windows.Navigation;
using System.Windows;
using RMM.Business.AccountService;
using RMM.Business.TransactionService;
using RMM.Business.OptionService;
using RMM.Business.DatabaseService;
using RMM.Business;
using System.Linq;
using RMM.Phone.ExtensionMethods;
using System.Collections.Generic;
using RMM.Phone.Execution;
using RMM.Business.GeneralService;
using System.IO.IsolatedStorage;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Resources;



namespace RMM.Phone.ViewModel
{
    public class MainViewModel : BugnionReverseViewModelBase
    {
        #region HEADER PANORAMA

        private string accountHeader = "Account";
        public string AccountHeader
        {
            get { return accountHeader; }
            set { accountHeader = value; }
        }

        private string favTransactionHeader = "Favorite";
        public string FavTransactionHeader
        {
            get { return favTransactionHeader; }
            set { favTransactionHeader = value; }
        }

        private string categoryHeader = "Category";
        public string CategoryHeader
        {
            get { return categoryHeader; }
            set { categoryHeader = value; }
        }

        private string optionHeader = "Option";
        public string OptionHeader
        {
            get { return optionHeader; }
            set { optionHeader = value; }
        }

        #endregion

        #region TEXTOPTION

        private string isPasswordTxt = "Password :";
        public string IsPasswordTxt
        {
            get { return isPasswordTxt; }
            set { isPasswordTxt = value; }
        }

        private string isSynchroTxt = "Synchronisation :";
        public string IsSynchroTxt
        {
            get { return isSynchroTxt; }
            set { isSynchroTxt = value; }
        }

        private string isTileTxt = "Tile :";
        public string IsTileTxt
        {
            get { return isTileTxt; }
            set { isTileTxt = value; }
        }

        private string isReportTxt = "Report :";
        public string IsReportTxt
        {
            get { return isReportTxt; }
            set { isReportTxt = value; }
        }

        private string isComparatorTxt = "Comparator :";
        public string IsComparatorTxt
        {
            get { return isComparatorTxt; }
            set { isComparatorTxt = value; }
        }

        #endregion

        #region COMMAND

        public RelayCommand<SelectionChangedEventArgs> AccountSelectedCommand { get; set; }
        public RelayCommand<SelectionChangedEventArgs> CategorySelectedCommand { get; set; }

        public RelayCommand<AccountViewData> EditAccountCommand { get; set; }
        public RelayCommand<AccountViewData> DeleteAccountCommand { get; set; }
        public RelayCommand<AccountViewData> FavoriteAccountCommand { get; set; }

        public RelayCommand<CategoryViewData> EditCategoryCommand { get; set; }
        public RelayCommand<CategoryViewData> DeleteCategoryCommand { get; set; }
        public RelayCommand<CategoryViewData> FavoriteCategoryCommand { get; set; }

        #endregion

        #region PROPFULL SUR OC<ViewData>, FAVORI ViewData, Option ViewData

        private ObservableCollection<AccountViewData> listeAccount;

        public ObservableCollection<AccountViewData> ListeAccount
        {
            get { return listeAccount; }
            set
            {
                listeAccount = value;
                RaisePropertyChanged("ListeAccount");
            }
        }

        private ObservableCollection<CategoryViewData> listeCategory;

        public ObservableCollection<CategoryViewData> ListeCategory
        {
            get { return listeCategory; }
            set
            {
                listeCategory = value;
                RaisePropertyChanged("ListeCategory");
            }
        }


        private AccountViewData favoriteAccountViewData;

        public AccountViewData FavoriteAccountViewData
        {
            get { return favoriteAccountViewData; }
            set
            {
                favoriteAccountViewData = value;
                RaisePropertyChanged("FavoriteAccountViewData");
            }
        }

        private OptionViewData optionViewData;

        public OptionViewData OptionViewData
        {
            get { return optionViewData; }
            set
            {
                optionViewData = value;
            }
        }

        #endregion


        #region Services

        public IDatabaseService DatabaseService { get; set; }
        public IAccountService AccountService { get; set; }
        public ICategoryService CategoryService { get; set; }
        public IOptionService OptionService { get; set; }
        public ITransactionService TransactionService { get; set; }

        #endregion

        public MainViewModel(IAccountService accountService, ICategoryService categoryService, ITransactionService transactionService, IOptionService optionService, IDatabaseService databaseService)
        {

            #region Set des objects obligatoire � la vue et ViewModel

            this.AccountSelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleAccountTaskSelected(args));
            this.CategorySelectedCommand = new RelayCommand<SelectionChangedEventArgs>((args) => HandleCategoryTaskSelected(args));

            this.EditAccountCommand = new RelayCommand<AccountViewData>((args) => HandleEditAccountTaskSelected(args));
            this.DeleteAccountCommand = new RelayCommand<AccountViewData>((args) => HandleDeleteAccountTaskSelected(args));
            this.FavoriteAccountCommand = new RelayCommand<AccountViewData>((args) => HandleFavoriteAccountTaskSelected(args));

            this.EditCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleEditCategoryTaskSelected(args));
            this.DeleteCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleDeleteCategoryTaskSelected(args));
            this.FavoriteCategoryCommand = new RelayCommand<CategoryViewData>((args) => HandleFavoriteCategoryTaskSelected(args));


            DatabaseService = databaseService;
            AccountService = accountService;
            TransactionService = transactionService;
            CategoryService = categoryService;
            OptionService = optionService;



            this.ListeAccount = new ObservableCollection<AccountViewData>();
            this.ListeCategory = new ObservableCollection<CategoryViewData>();

            #endregion


            #region FAKE DATA

            var isAlreadyCreated = DatabaseService.Initialize();

            if (isAlreadyCreated)
            {
                DumpMyDBSQLCE.ProcessDatasOnDB(AccountService, CategoryService, TransactionService, OptionService);

                //FAVORI COMPTE 1
                OptionService.SetFavoriteIdAccount(1);
            }

            #endregion

            ExecuteSafeDispatcher(() => SetOption(), () => RefreshAccountAndFavori(), () => RefreshCategory());
        }

        public void RefreshAccountAndFavori()
        {
            this.ListeAccount = new ObservableCollection<AccountViewData>();
            SetListAccount();
            SetFavori();
        }

        public void RefreshCategory()
        {
            this.ListeCategory = new ObservableCollection<CategoryViewData>();
            SetListCategory();
        }


        private void SetOption()
        {
            // Get des options � binder
            var resultOption = OptionService.GetOption();

            if (resultOption.IsValid)
                OptionViewData = resultOption.Value.ToOptionViewData();

        }
        private void SetListAccount()
        {
            //Get des options en Minimal ( Lazy loading sur les listes de transaction ) avec le true
            var resultatAccountService = AccountService.GetAllAccounts(true);
            if (resultatAccountService.IsValid)
            {
                resultatAccountService.Value.ForEach(
                    vd =>
                    {
                        this.ListeAccount.Add(vd.ToAccountViewData());
                    });
            }
        }
        private void SetListCategory()
        {
            //Get des categories en Minimal ( Lazy loading sur les listes de transaction ) avec le true
            var resultatCategoryService = CategoryService.GetAllCategories(true);
            if (resultatCategoryService.IsValid)
            {
                resultatCategoryService.Value.ForEach(
                    vd =>
                    {
                        this.ListeCategory.Add(vd.ToCategoryViewData());
                    });
            }
        }
        private void SetFavori()
        {
            //Set de la visibilite pour les favori : on check si le Favori Id dans l'object option contient un des id des comptes
            this.ListeAccount.ToList().ForEach(account =>
                {
                    if (account.Id == OptionViewData.Favorite)
                    {
                        account.Favorite = Visibility.Visible;
                        FavoriteAccountViewData = new AccountViewData();
                        FavoriteAccountViewData = account;
                    }
                    else
                    {
                        account.Favorite = Visibility.Collapsed;
                    }

                });

            // si le compte favori a �t� sett�, alors on eagger sur sa liste de prop
            if (FavoriteAccountViewData != null)
            {
                var donneMoiFavoriEnEagger = AccountService.GetAccountById(FavoriteAccountViewData.Id, false);
                if (donneMoiFavoriEnEagger.IsValid)
                    FavoriteAccountViewData = donneMoiFavoriEnEagger.Value.ToAccountViewData();

                if (FavoriteAccountViewData.ListTransaction.Count > 0)
                    FavoriteAccountViewData.Balance = FavoriteAccountViewData.ListTransaction.Sum(t => t.Amount);
            }
        }





        #region Handle on Task Selected

        private void HandleEditAccountTaskSelected(AccountViewData args)
        {
            if (args != null)
            {
                NavigateTo("/View/EditAccount.xaml?accountId={0}", args.Id);
            }

        }

        private void HandleDeleteAccountTaskSelected(AccountViewData args)
        {
            if (args != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to delete " + args.Name + "?", "Delete an account", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    TransactionService.DeleteTransactionsByAccountId(args.Id);
                    AccountService.DeleteAccountById(args.Id);
                    this.ListeAccount.Remove(args);
                }
            }
        }

        private void HandleFavoriteAccountTaskSelected(AccountViewData args)
        {
            if (args == null) { return; }

            OptionService.SetFavoriteIdAccount(args.Id);

            if (optionViewData.Favorite != args.Id)
            {
                OptionViewData.Favorite = args.Id;
                ExecuteSafeDispatcher(() => RefreshAccountAndFavori(), () => SetFavori());
            }



        }


        private void HandleEditCategoryTaskSelected(CategoryViewData args)
        {
            if (args != null)
            {
                NavigateTo("/View/EditCategory.xaml?categoryId={0}", args.Id);
            }
        }

        private void HandleDeleteCategoryTaskSelected(CategoryViewData args)
        {
            if (args != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you really want to delete " + args.Name + "?", "Delete a category", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    CategoryService.DeleteCategorieById(args.Id);
                    this.ListeCategory.Remove(args);
                }
            }
        }

        private void HandleFavoriteCategoryTaskSelected(CategoryViewData args)
        {
            //A ENLEVER
        }


        private void HandleAccountTaskSelected(SelectionChangedEventArgs args)
        {
            if (args.AddedItems[0] != null)
            {
                var account = args.AddedItems[0] as AccountViewData;
                NavigateTo("/View/AccountPivot.xaml?accountId={0}", account.Id);
            }
        }

        private void HandleCategoryTaskSelected(SelectionChangedEventArgs args)
        {
            if (args.AddedItems[0] != null)
            {
                var category = args.AddedItems[0] as CategoryViewData;
                NavigateTo("/View/CategoryPivot.xaml?categoryId={0}", category.Id);
            }
        }

        #endregion
    }
}