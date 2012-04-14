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
using RMM.Data;
using RMM.Business.CategoryService;
using RMM.Business.AccountService;
using RMM.Business.TransactionService;
using System.Collections.Generic;
using RMM.Business.OptionService;
using RMM.Data.Model;

namespace RMM.Business
{
    public class DumpMyDBSQLCE
    {

        public static void ProcessDatasOnDB(IAccountService AccountService, ICategoryService CategoryService, ITransactionService TransactionService, IOptionService OptionService)
        {
            #region Categories

            var c1 = new Category();
            c1.Balance = 7.0;
            c1.Color = "FFA640";
            c1.Name = "Vacances";

            var c2 = new Category();
            c2.Balance = 7.0;
            c2.Color = "FFA640";
            c2.Name = "Profesionnel";

            #endregion

            #region Accounts

            var na1 = new Account();
            na1.Balance = 7.0;
            na1.BankName = "Credit Agricole";
            na1.Name = "Mon compte courant";

            var na2 = new Account();
            na2.Balance = 7.0;
            na2.BankName = "HSBC";
            na2.Name = "Mon compte courant";

            var na3 = new Account();
            na3.Balance = 7.0;
            na3.BankName = "HSBC";
            na3.Name = "Mon compte epargne HSBC";

       

            #endregion

            #region DB Cleaner

            var Categories = CategoryService.GetAllCategories(true);

            var Accounts = AccountService.GetAllAccounts(true);

            if (Categories.Value.Count > 0)
            {
                foreach (var entity in Categories.Value)
                {
                    CategoryService.DeleteCategorieById(entity.ID);
                }
            }

            if (Accounts.Value.Count > 0)
            {
                foreach (var entity in Accounts.Value)
                {
                    AccountService.DeleteAccountById(entity.ID);

                }
            }

            #endregion

            #region Ajout Données

            na1 = AccountService.CreateAccount(new CreateAccountCommand() { BankName = na1.BankName, Name = na1.Name}).Value;
            na2 = AccountService.CreateAccount(new CreateAccountCommand() { BankName = na2.BankName, Name = na2.Name}).Value;
            na3 = AccountService.CreateAccount(new CreateAccountCommand() { BankName = na3.BankName, Name = na3.Name}).Value;

            c1 = CategoryService.CreateCategory(new CreateCategoryCommand() { Name = c1.Name, Color = c1.Color }).Value;
            c2 = CategoryService.CreateCategory(new CreateCategoryCommand() { Name = c2.Name, Color = c2.Color }).Value;

            #endregion



            #region Ajout 26 transactions

            var t1 = new Transaction() { Name = "redevance tv", Account = na1, Amount = -100, Description="test de la description" };
            var t2 = new Transaction() { Name = "les courses", Account = na1, Amount = -80, Description = "test de la description" };
            var t3 = new Transaction() { Name = "restau", Account = na2, Category = c2, Amount = -40, Description = "test de la description" };
            var t4 = new Transaction() { Name = "piscine", Account = na1, Amount = -10, Description = "test de la description" };
            var t5 = new Transaction() { Name = "ciné", Account = na1, Amount = -5, Description = "test de la description" };
            var t6 = new Transaction() { Name = "bar", Account = na1, Category = c1, Amount = -10.5, Description = "test de la description" };
            var t7 = new Transaction() { Name = "club", Account = na1, Category = c1, Amount = -15.5, Description = "test de la description" };
            var t8 = new Transaction() { Name = "lotto", Account = na1, Amount = 7, Description = "test de la description" };
            var t9 = new Transaction() { Name = "la biere", Account = na1, Amount = -10, Description = "test de la description" };
            var t10 = new Transaction() { Name = "bouquet de fleur", Account = na1, Amount = -20, Description = "test de la description" };
            var t11 = new Transaction() { Name = "medicaments", Account = na1, Amount = -5, Description = "test de la description" };
            var t12 = new Transaction() { Name = "pizza tv", Account = na1, Amount = -10, Description = "test de la description" };
            var t13 = new Transaction() { Name = "moulin de la forge tv", Account = na1, Category = c1, Amount = -150, Description = "test de la description" };
            var t14 = new Transaction() { Name = "licence MS", Account = na2, Category = c2, Amount = -35, Description = "test de la description" };
            var t15 = new Transaction() { Name = "James", Account = na2, Category = c2, Amount = 500, Description = "test de la description" };
            var t16 = new Transaction() { Name = "Ian", Account = na2, Category = c2, Amount = 1000, Description = "test de la description" };
            var t17 = new Transaction() { Name = "Villa", Account = na2, Category = c1, Amount = -200, Description = "test de la description" };
            var t18 = new Transaction() { Name = "Telephone", Account = na2, Category = c2, Amount = -30, Description = "test de la description" };
            var t19 = new Transaction() { Name = "park d'actraction", Account = na2, Amount = -20, Description = "test de la description" };
            var t20 = new Transaction() { Name = "enfants", Account = na2, Amount = 46.7, Description = "test de la description" };
            var t21 = new Transaction() { Name = "nouvelle maison", Account = na3, Amount = -33.6, Description = "test de la description" };
            var t22 = new Transaction() { Name = "epargne taux fixe", Account = na3, Amount = 10, Description = "test de la description" };
            var t23 = new Transaction() { Name = "epargne sur montpellier", Account = na3, Amount = 60, Description = "test de la description" };
            var t24 = new Transaction() { Name = "epargne de Paris", Account = na3, Amount = -20, Description = "test de la description" };
            var t25 = new Transaction() { Name = "investissement", Account = na3, Amount = 250, Description = "test de la description" };
            var t26 = new Transaction() { Name = "import maman", Account = na1, Amount = 700, Description = "test de la description" };

            var listDeTransaction = new List<Transaction>();
            listDeTransaction.Add(t1);
            listDeTransaction.Add(t2);
            listDeTransaction.Add(t3);
            listDeTransaction.Add(t4);
            listDeTransaction.Add(t5);
            listDeTransaction.Add(t6);
            listDeTransaction.Add(t7);
            listDeTransaction.Add(t8);
            listDeTransaction.Add(t9);
            listDeTransaction.Add(t10);
            listDeTransaction.Add(t11);
            listDeTransaction.Add(t12);
            listDeTransaction.Add(t13);
            listDeTransaction.Add(t14);
            listDeTransaction.Add(t15);
            listDeTransaction.Add(t16);
            listDeTransaction.Add(t17);
            listDeTransaction.Add(t18);
            listDeTransaction.Add(t19);
            listDeTransaction.Add(t20);
            listDeTransaction.Add(t21);
            listDeTransaction.Add(t22);
            listDeTransaction.Add(t23);
            listDeTransaction.Add(t24);
            listDeTransaction.Add(t25);
            listDeTransaction.Add(t26);


            #endregion



            listDeTransaction.ForEach(transac => 
                
                {
            var newtransaction = new CreateTransactionCommand()
            {
                Name = transac.Name,
                accountId = transac.Account.ID,
                Amount = transac.Amount,
                Description = transac.Description
            };

                    if (transac.Category != null)
                    {
                        newtransaction.categoryId = transac.Category.ID;
                    }

                    TransactionService.CreateTransaction(newtransaction);
                });

            OptionService.SetFirstTimeOption();
        }

    }
}
