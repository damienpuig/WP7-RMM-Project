using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Business.Helpers;
using RMM.Data.Model;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace RMM.Business.AccountService
{
    public class AccountService: IAccountService
    {
        private RmmDataContext dataContext = null;

        public RmmDataContext DataContext
        {
            get { return dataContext; }
            set
            {
                dataContext = value;
                dataContext.ObjectTrackingEnabled = true;
                dataContext.Log = Console.Out;
            }
        }
        

        public Result<Account> DeleteAccountById(int accountId)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                 
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var account = (from t in DataContext.Account
                                    where t.ID == accountId
                                    select t).First();

                    if (account != null)
                    {
                        DataContext.Account.Log().DeleteOnSubmit(account);
                        DataContext.SubmitChanges();
                    }

                    result.Value = account;
                }

            }, () => "error");
        }

        public Result<Account> GetAccountById(int accountId, bool OnMinimal)
        {
            
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Account>(acc => acc.TransactionList);

                    var account = DataContext.Account.Log().Where(a => a.ID == accountId).First();


                    result.Value = account;
                }

            }, () => "error");
        }

        public Result<Account> CreateAccount(CreateAccountCommand newAccountCommand)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newAccountEntity = new Account();
                    newAccountEntity.Name = newAccountCommand.Name;
                    newAccountEntity.BankName = newAccountCommand.BankName;
                    newAccountEntity.CreatedDate = DateTime.Now;
                    newAccountEntity.Balance = 0;


                    DataContext.Account.InsertOnSubmit(newAccountEntity);

                    DataContext.SubmitChanges();

                    var AddedAccount = DataContext.Account.Log().Where(a => a.CreatedDate == newAccountEntity.CreatedDate).First();

                    result.Value = AddedAccount;

                }

            }, () => "error");
        }

        public Result<Account> UpdateAccount(EditAccountCommand editAccountCommand)
        {
            return Result<Account>.SafeExecute<AccountService>(result =>
            {

                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = DataContext.Account.Log().Where(t => t.ID == editAccountCommand.id).First();

                    entityToUpdate.ID = editAccountCommand.id;
                    entityToUpdate.Name = editAccountCommand.Name;
                    entityToUpdate.BankName = editAccountCommand.BankName;
                    entityToUpdate.CreatedDate = DateTime.Now;


                    DataContext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "error");
        }


        public Result<List<Account>> GetAllAccounts(bool OnMinimal)
        {
            return Result<List<Account>>.SafeExecute<AccountService>(result =>
            {
                DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING);

                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                          if (!OnMinimal)
                              DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Account>(Acc => Acc.TransactionList);

                          var accounts = DataContext.Account.Log().ToList();

                          result.Value = accounts;
                }

            }, () => "error");
        }


    }
}
