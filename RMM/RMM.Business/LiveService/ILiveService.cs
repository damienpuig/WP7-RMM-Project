using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Live;
using System.Security.Cryptography;
using RMM.Data.Model;

namespace RMM.Business.LiveService
{
    public interface ILiveService
    {

        /// <summary>
        /// Backup la base actuelle sur skyDrive
        /// </summary>
        /// <returns></returns>
        Result<SkyDriveBackup> BackupToSkyDrive(LiveConnectSession session);

        /// <summary>
        /// restore la base depuis skydrive: attention bloquer l'application et mettre en place un systeme de check au demarrage
        /// </summary>
        /// <param name="session"></param>
        /// <returns>retourne la backup utilisée pour l'operation</returns>
        Result<SkyDriveBackup> RestoreBackupFromSkyDrive(LiveConnectSession session, int bakcupId);

        /// <summary>
        /// retourne la liste des backups disponible sur skydrive en minimal, pas de chargement du flux de données.
        /// </summary>
        /// <returns>retourne une liste de d'object SkyDriveBackup minimale, sans MemoryStream</returns>
        Result<List<SkyDriveBackup>> GetAllBackupsFromSkyDrive(LiveConnectSession session);


        /// <summary>
        /// Va supprimer la backup sur SkyDrive
        /// </summary>
        /// <param name="session"></param>
        /// <param name="IdBackup"></param>
        /// <returns></returns>
        Result<bool> DeleteBackupOnSkyDrive(LiveConnectSession session, int IdBackup);

        /// <summary>
        /// Supprime TOUTE les backup sur SkyDrive
        /// </summary>
        /// <param name="session"></param>
        /// <param name="IdBackup"></param>
        /// <returns></returns>
        Result<bool> DeleteAllBackupOnSkyDrive(LiveConnectSession session);


        /// <summary>
        /// Regarde si il y a le reseau
        /// </summary>
        /// <returns></returns>
        bool CheckNetWork();

        
        




       
    }
}
