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
using System.Threading;
using Microsoft.Live;
using System.Threading.Tasks;

namespace RMM.Business.ServiceBase
{
    public class WorkService : IWorkService
    {

        public bool Process<T>(Action<T, object> work, object etatObject)
        {
            try
            {
                Task.Factory.StartNew(id =>
                {

                    //Donne a l'action le service instancier
                    var service = Activator.CreateInstance<T>();
                    work(service, id);



                }, etatObject);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
