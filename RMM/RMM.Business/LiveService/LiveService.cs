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

namespace RMM.Business.LiveService
{
    public class LiveService : ILiveService
    {
        public Result<SkyDriveBackupDto> BackupToSkyDriveNow(LiveConnectSession session)
        {
            throw new NotImplementedException();
        }

        public Result<SkyDriveBackupDto> RestoreBackupFromSkyDriveNow(LiveConnectSession session)
        {
            throw new NotImplementedException();
        }

        public Result<System.Collections.Generic.List<SkyDriveBackupDto>> GetBackupsFromSkyDriveMinimal(LiveConnectSession session)
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

        public Result<SkyDriveBackupDto> CheckLastSkyDriveSync(int lastIdBackup)
        {
            throw new NotImplementedException();
        }
    }
}
