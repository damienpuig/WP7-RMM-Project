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

namespace RMM.Business.LiveService
{
    public class SkyDriveBackup
    {
        /// <summary>
        /// Fichier provenant de SkyDrive
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Date de sauvegarde
        /// </summary>
        public DateTime SavedDate  { get; set; }

    }
}
