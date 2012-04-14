/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:RMM.Phone.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;
using RMM.Business.TransactionService;
using RMM.Business.OptionService;
using RMM.Business.DatabaseService;
using RMM.Phone.Execution;
using RMM.Business.GeneralService;

namespace RMM.Phone.BusinessModel
{

    public class BusinessBootStrapper
    {
        static BusinessBootStrapper()
        {
            if(ServiceLocator.Current == null)
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Register Services
            SimpleIoc.Default.Register<IAccountService, AccountService>();
            SimpleIoc.Default.Register<ICategoryService, CategoryService>();
            SimpleIoc.Default.Register<IOptionService, OptionService>();
            SimpleIoc.Default.Register<ITransactionService, TransactionService>();
            SimpleIoc.Default.Register<IDatabaseService, DatabaseService>();
            SimpleIoc.Default.Register<IThreadSafeGeneral, ThreadSafeGeneral>();
            #endregion

            //Register mappings
           // AutoMapperRegistry.Configure();
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            //que c'est pas tres beau
        }
    }
}