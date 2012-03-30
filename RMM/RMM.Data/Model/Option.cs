using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace RMM.Data.Model
{
    [Table(Name="Option")]
    public class Option
    {
         //IsDbGenerated = true   1 SEUL FICHIER D'OPTION
        [Column(IsPrimaryKey = true)]
        public int id { get; set; }

        [Column]
        public bool IsPassword { get; set; }

        [Column]
        public bool IsSynchro { get; set; }

        [Column]
        public DateTime RefreshTimeBackup { get; set; }

        [Column]
        public int LastBackupVersion { get; set; }

        [Column]
        public bool IsPrimaryTile { get; set; }


        [Column]
        public int Favorite { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }
    }
}
