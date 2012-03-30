using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RMM.Business.TransactionService;
using RMM.Phone.ViewData.Account;
using RMM.Business.AccountService;
using RMM.Business.OptionService;
using RMM.Business.CategoryService;
using RMM.Data.Model;
using System.Linq;
using System.Collections.Generic;

namespace RMM.Phone.ExtensionMethods
{
    public static class ObjectsExtensionsToViewData
    {
        public static TransactionViewData ToTransactionViewData(this Transaction Objectsource, bool OnDetails)
        {
            var newTransactionViewData = new TransactionViewData();

            newTransactionViewData.Id = Objectsource.ID;
            newTransactionViewData.Name = Objectsource.Name;

            if (Objectsource.Account != null && OnDetails)
                newTransactionViewData.Account = Objectsource.Account.ToAccountViewData() ;

            if (Objectsource.Category != null && OnDetails)
                newTransactionViewData.Category = Objectsource.Category.ToCategoryViewData();


            newTransactionViewData.Description = Objectsource.Description;
            newTransactionViewData.Amount = Objectsource.Amount;
            newTransactionViewData.CreatedDate = Objectsource.CreatedDate.ToShortDateString();

            return newTransactionViewData;

        }

        public static TransactionViewData ToTransactionMinimalViewData(this Transaction Objectsource)
        {
            var newTransactionViewData = new TransactionViewData();

            newTransactionViewData.Id = Objectsource.ID;
            newTransactionViewData.Name = Objectsource.Name;
            newTransactionViewData.Description = Objectsource.Description;
            newTransactionViewData.Amount = Objectsource.Amount;
            newTransactionViewData.CreatedDate = Objectsource.CreatedDate.ToShortDateString();

            return newTransactionViewData;

        }

        public static AccountViewData ToAccountViewData(this Account Objectsource)
        {


            var newAccountViewData = new AccountViewData();

            newAccountViewData.Id = Objectsource.ID;
            newAccountViewData.Name = Objectsource.Name;
            newAccountViewData.BankName = Objectsource.BankName;
            newAccountViewData.Balance = Objectsource.Balance;


            if (Objectsource.TransactionList.HasLoadedOrAssignedValues)
            {
                newAccountViewData.ListTransaction = Objectsource.TransactionList.Select(t => t.ToTransactionMinimalViewData()).ToList();
            }


            //ATTENTION, LE SET DU FAVORITE SE FAIT DANS LA PAGE APPROPRIE, VIA LE OptionService

            return newAccountViewData;
        }

        public static OptionViewData ToOptionViewData(this Option Objectsource)
        {
            var newOptionViewData = new OptionViewData();
            newOptionViewData.IsPassword = Objectsource.IsPassword;
            newOptionViewData.IsPrimaryTile = Objectsource.IsPrimaryTile;
            newOptionViewData.IsSynchro = Objectsource.IsSynchro;
            newOptionViewData.Favorite = Objectsource.Favorite;
            newOptionViewData.RefreshTimeBackup = Objectsource.RefreshTimeBackup;

            return newOptionViewData;
        }

        public static CategoryViewData ToCategoryViewData(this Category Objectsource)
        {
            var newCategoryViewData = new CategoryViewData();

            newCategoryViewData.Id = Objectsource.ID;
            newCategoryViewData.Name = Objectsource.Name;
            newCategoryViewData.Balance = Objectsource.Balance;
            newCategoryViewData.Color = Objectsource.Color;

            if (Objectsource.TransactionList.HasLoadedOrAssignedValues)
            {
                newCategoryViewData.ListTransaction = Objectsource.TransactionList.Select(t => t.ToTransactionMinimalViewData()).ToList();
            }


            return newCategoryViewData;
        }
    }
}
