using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;

namespace RMM.Business
{
    public class Result<T>
    {
        public bool IsValid { get; set; }
        public T Value { get; set; }

        public List<Erreur> Errors { get; set; }

        public Result()
        {
            Errors = new List<Erreur>();
        }

        public Result(List<Erreur> errors)
        {
            Errors = errors;
        }


        public static Result<T> SafeExecute<Tinvocation>(Action<Result<T>> codeAExecuter, Func<string> errorMessageFunc)
        {
            var resultat = new Result<T>();

            try
            {
                 codeAExecuter(resultat);

                resultat.IsValid = true;


            }
            catch (Exception except)
            {
                resultat.Errors.Add(new Erreur() { InvocationService = typeof(Tinvocation).ToString(), Message = except.Message });
                resultat.IsValid = false;
            }

            return resultat;
        }

    }
}
