using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;
using System.Data.Linq;
using RMM.Business.Helpers;
using System.Linq.Expressions;
using System.IO;
using System.Windows;



namespace RMM.Business.TransactionService
{
    public class TransactionService : ITransactionService
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


        public Result<Transaction> DeleteTransactionById(int transactionDtoId)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
                {

                    using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Account);

                        var transaction = (from t in DataContext.Transaction.Log()
                                           where t.ID == transactionDtoId
                                           select t).First();



                        if (transaction != null)
                        {
                            transaction.Account.Balance -= transaction.Amount;
                            DataContext.Transaction.Log().DeleteOnSubmit(transaction);
                            DataContext.SubmitChanges();
                        }


                        result.Value = transaction;
                    }

                }, () => "erreur");
        }

        public Result<Transaction> GetTransactionById(int transactionId, bool OnMinimal)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
                {
                    using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        if (!OnMinimal)
                            DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Category, t => t.Account);

                        var transaction = DataContext.Transaction.Log().Where(t => t.ID == transactionId).First();


                        result.Value = transaction;
                    }
                }, () => "erreur");
        }

        public Result<List<Transaction>> GetTransactionsByCategoryId(int categoryId, bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Account, t => t.Category);


                    var transactions = (from t in DataContext.Transaction.Log()
                                        where t.Category.ID == categoryId
                                        select t).ToList();


                    result.Value = transactions;
                }
            }, () => "erreur");
        }

        public Result<List<Transaction>> GetTransactionsByAccountId(int accountId, bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
              {
                  using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                  {
                          if (!OnMinimal)
                              DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Category, t => t.Account);

                          var transactions = DataContext.Transaction.Log().Where(t => t.Account.ID == accountId).ToList();

                          result.Value = transactions;

                  }
              }, () => "erreur");
        }

        public Result<Transaction> CreateTransaction(CreateTransactionCommand newTransactionCommand)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = new Transaction();

                    Category attachedCategoryEntity;
                    Account attachedAccountEntity;

                    if (newTransactionCommand.categoryId.HasValue)
                    {
                        attachedCategoryEntity = DataContext.Category.Log().Where(c => c.ID == newTransactionCommand.categoryId).First();
                        transaction.Category = attachedCategoryEntity;
                    }

                    if (newTransactionCommand.accountId != 0)
                    {
                        attachedAccountEntity = DataContext.Account.Log().Where(c => c.ID == newTransactionCommand.accountId).First();
                        attachedAccountEntity.Balance += newTransactionCommand.Amount;
                        transaction.Account = attachedAccountEntity;
                    }

                    transaction.Amount = newTransactionCommand.Amount;
                    transaction.Description = newTransactionCommand.Description;
                    transaction.Name = newTransactionCommand.Name;
                    transaction.CreatedDate = DateTime.Now;

                    DataContext.Transaction.InsertOnSubmit(transaction);
                    DataContext.SubmitChanges();

                    var AddedTransac = DataContext.Transaction.Log().Where(a => a.CreatedDate == transaction.CreatedDate).First();

                    result.Value = AddedTransac;

                }
            }, () => "erreur");


        }

        public Result<Transaction> UpdateTransaction(EditTransactionCommand EditTransactionCommand)
        {
            return Result<Transaction>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    DataLoadOptions options = new DataLoadOptions();
                    options.LoadWith<Transaction>(c => c.Category);
                    options.LoadWith<Transaction>(c => c.Account);

                    DataContext.LoadOptions = options;


                    var entityToUpdate = DataContext.Transaction.Log().Where(t => t.ID == EditTransactionCommand.id).First();


                    entityToUpdate.Name = EditTransactionCommand.Name;

                    if (entityToUpdate.Category.ID != EditTransactionCommand.categoryId)
                    {
                        var categoryAttachedByUpdate = DataContext.Category.Log().Where(c => c.ID == EditTransactionCommand.categoryId).First();
                        entityToUpdate.Category = categoryAttachedByUpdate;
                    }


                    if (entityToUpdate.Account.ID != EditTransactionCommand.accountId)
                    {
                        var accountAttachedByUpdate = DataContext.Account.Log().Where(c => c.ID == EditTransactionCommand.accountId).First();
                        entityToUpdate.Account = accountAttachedByUpdate;
                    }

                    if (string.IsNullOrEmpty(EditTransactionCommand.Description))
                        entityToUpdate.Description = EditTransactionCommand.Description;

                    entityToUpdate.Amount = EditTransactionCommand.Amount;

                    entityToUpdate.CreatedDate = DateTime.Now;

                    DataContext.SubmitChanges();

                    result.Value = entityToUpdate;

                }
            }, () => "erreur");


        }

        public Result<List<Transaction>> GetAllTransactions(bool OnMinimal)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    if (!OnMinimal)
                        DataContext.LoadOptions = DBHelpers.GetConfigurationLoader<Transaction>(t => t.Account, t => t.Category);

                    var query = DataContext.Transaction.Log().ToList();

                    result.Value = query;
                }
            }, () => "erreur");
        }

        public Result<List<Transaction>> DeleteTransactionsByAccountId(int accountId)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = DataContext.Transaction.Log().Where(t => t.AccountID == accountId).ToList();
                    var account = DataContext.Account.Log().Where(a => a.Balance == accountId).First();

                    if (transaction != null)
                    {
                        account.Balance = 0;
                        DataContext.Transaction.Log().DeleteAllOnSubmit(transaction);
                        DataContext.SubmitChanges();
                    }


                    result.Value = transaction;
                }
            }, () => "erreur");
        }

        public Result<List<Transaction>> DeleteTransactionsByCategoryId(int categoryId)
        {
            return Result<List<Transaction>>.SafeExecute<TransactionService>(result =>
            {
                using (DataContext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var transaction = DataContext.Transaction.Log().Where(t => t.CategoryID == categoryId).ToList();
                    var category = DataContext.Category.Log().Where(t => t.ID == categoryId).First();


                    if (transaction != null)
                    {
                        category.Balance = 0;
                        DataContext.Transaction.Log().DeleteAllOnSubmit(transaction);
                        DataContext.SubmitChanges();
                    }


                    result.Value = transaction;
                }
            }, () => "erreur");
        }
    }
}

