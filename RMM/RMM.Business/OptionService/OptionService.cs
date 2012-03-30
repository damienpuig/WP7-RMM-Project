using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;

namespace RMM.Business.OptionService
{
    public class OptionService : IOptionService
    {
        private RmmDataContext datacontext = null;

        public Result<Option> GetOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var option = (from t in datacontext.Option
                                  select t).First();

                    result.Value = option;
                }

            }, () => "erreur");
        }

        public Result<Option> UpdateOption(Option optionToUpdate)
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var entityToUpdate = datacontext.Option.First();

                    entityToUpdate.IsPassword = optionToUpdate.IsPassword;
                    entityToUpdate.IsPrimaryTile = optionToUpdate.IsPrimaryTile;
                    entityToUpdate.IsSynchro = optionToUpdate.IsSynchro;
                    entityToUpdate.Favorite = optionToUpdate.Favorite;
                    entityToUpdate.ModifiedDate = DateTime.Now;
                    entityToUpdate.RefreshTimeBackup = optionToUpdate.RefreshTimeBackup;

                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "erreur");


        }


        public Result<Option> SetFirstTimeOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newFirstTimeOption = new Option()
                    {
                        id = 1,
                        IsPrimaryTile = false,
                        IsPassword = false,
                        IsSynchro = false,
                        Favorite = 0,
                        LastBackupVersion = 0,
                        ModifiedDate = DateTime.Now,
                        RefreshTimeBackup = DateTime.Now
                    };

                    datacontext.Option.InsertOnSubmit(newFirstTimeOption);

                    datacontext.SubmitChanges();

                    result.Value = newFirstTimeOption;
                }
            }, () => "erreur");


        }


        public Result<int> GetFavoriteIdAccount()
        {

            return Result<int>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var idFavorite = (from t in datacontext.Option
                                      select t.Favorite).First();

                    result.Value = idFavorite;
                }

            }, () => "erreur");

        }


        public Result<int> SetFavoriteIdAccount(int AccountId)
        {
            return Result<int>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = datacontext.Option.First();

                    entityToUpdate.Favorite = AccountId;

                    

                    datacontext.SubmitChanges();

                    result.Value = AccountId;
                }

            }, () => "erreur");
        }
    }
}
