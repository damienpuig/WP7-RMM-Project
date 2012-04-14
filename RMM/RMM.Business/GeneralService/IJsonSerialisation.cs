using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.GeneralService
{
    public interface IJsonSerialisation
    {
        string returnJson(object entree);

        T returnObject<T>(string objectFromstringToReturn);
    }
}
