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
using System.Data.Linq;
using RMM.Data;
using System.Reflection;
using System.Diagnostics;

namespace RMM.Business
{
    public static class LogExtensions
    {
        public static int compteurRequete = 0;


        public static Table<TEntity> Log<TEntity>(this Table<TEntity> source) where TEntity : class
        {
            //StackTrace stackTrace = new StackTrace();
            //string methodBaseName = stackTrace.GetFrame(6).GetMethod().Name;

            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();

            compteurRequete++;
            source.Context.Log.WriteLine(string.Format("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! FOR {0} FROM {2} !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! NUMBER = {1} ", typeof(TEntity).Name, compteurRequete, methodBase.Name));
            return source;
        }

    }
}
