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
using Microsoft.Live;
using RMM.Data.Model;
using RMM.Business.GeneralService;
using Newtonsoft.Json;

namespace RMM.Business.LiveService
{
    public class LiveService : ILiveService, IJsonSerialisation
    {

        public Result<SkyDriveBackup> BackupToSkyDrive(LiveConnectSession session)
        {
            throw new NotImplementedException();
        }

        public Result<SkyDriveBackup> RestoreBackupFromSkyDrive(LiveConnectSession session, int bakcupId)
        {
            throw new NotImplementedException();
        }

        public Result<System.Collections.Generic.List<SkyDriveBackup>> GetAllBackupsFromSkyDrive(LiveConnectSession session)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteBackupOnSkyDrive(LiveConnectSession session, int IdBackup)
        {
            throw new NotImplementedException();
        }

        public Result<bool> DeleteAllBackupOnSkyDrive(LiveConnectSession session)
        {
            throw new NotImplementedException();
        }


        public bool CheckNetWork()
        {
            throw new NotImplementedException();
        }

        private byte[] CreateTempFile()
        {
            return null;
        }

        private bool DeleteTempFile()
        {
            return false;
        }

        /// <summary>
        /// Creer L'arborescence sur SKyDrive a savoir:
        /// 
        /// Easy Money 
        /// ----------------------------------------------------------------------------
        /// |  Fichier Readme
        /// |
        /// |Backups dans le dossier suivant
        /// |---------------------------------------------------------------------------
        /// | fichiers avec pour pattern: 
        /// | - 2012-01-01-EasyMoneyBackUp.txt
        /// | - 2012-01-02-EasyMoneyBackUp.txt
        /// </summary>
        /// <returns></returns>
        private bool CreateFirstTreeOnSkyDrive()
        {
            return false;
        }


        /// <summary>
        /// Retourne un String ( peut etre une liste ? ) sous forme JSON
        /// </summary>
        /// <param name="entree"></param>
        /// <returns></returns>
        public string returnJson(object entree)
        {
            return JsonConvert.SerializeObject(entree, Formatting.Indented);
        }

        /// <summary>
        /// Retourne un object de type T ( peut etre une liste ?) sous forme JSON  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectFromstringToReturn"></param>
        /// <returns></returns>
        public T returnObject<T>(string objectFromstringToReturn)
        {
            return JsonConvert.DeserializeObject<T>(objectFromstringToReturn);
        }
    }
}
