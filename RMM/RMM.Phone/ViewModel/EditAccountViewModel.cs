﻿using GalaSoft.MvvmLight;
using RMM.Phone.ViewData.Account;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using RMM.Business.AccountService;
using RMM.Phone.ExtensionMethods;
using RMM.Business.CategoryService;

namespace RMM.Phone.ViewModel
{

    public class EditAccountViewModel : ViewModelBase
    {
        public AccountViewData Account { get; set; }

        public RelayCommand DeleteAllTransactionCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public IAccountService Accountservice { get; set; }

        public EditAccountViewModel(IAccountService accountService)
        {
            Accountservice = accountService;

            DeleteAllTransactionCommand = new RelayCommand(() => HandleDeleteAllTransactionTaskSelected());
            UpdateCommand = new RelayCommand(() => HandleUpdateTaskSelected());
            CancelCommand = new RelayCommand(() => HandleCancelTaskSelected());
        }

        public void SelectIndex(string accountId)
        {
            var IntToGo = int.Parse(accountId);

            var selectedAccount = Accountservice.GetAccountById(IntToGo, true);

            if (selectedAccount.IsValid)
                Account = selectedAccount.Value.ToAccountViewData();

            RaisePropertyChanged("Account");
            
        }

        void HandleDeleteAllTransactionTaskSelected()
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to delete this account ?", "delete " + Account.Name, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                //DELETE ALL
            }
        }

        void HandleUpdateTaskSelected()
        {
            var editAccountCommand = new EditAccountCommand(){ BankName = Account.BankName, id= Account.Id, Name= Account.Name, PhotoUrl= Account.PhotoUrl};
            var result =  Accountservice.UpdateAccount(editAccountCommand);

            if (result.IsValid)
            {
                var rootFrame = (App.Current as App).RootFrame;
                rootFrame.Navigate(new System.Uri("/MainPage.xaml?update=account", System.UriKind.Relative));
            }
        }

        void HandleCancelTaskSelected()
        {
            var rootFrame = (App.Current as App).RootFrame;
            rootFrame.Navigate(new System.Uri("/MainPage.xaml", System.UriKind.Relative));
        }
    }
}