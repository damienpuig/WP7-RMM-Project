using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.GeneralService
{
   public interface IWorkService
    {
       /// <summary>
       /// Procede sur un Thread a part un action precise. Dans ce cas la, nous aurons le processing de backup et de Restore.
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="work"></param>
       /// <param name="etatObject"></param>
       bool Process<T>(Action<T, object> work, object etatObject);
    }
}
