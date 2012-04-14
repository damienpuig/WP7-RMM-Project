using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Newtonsoft.Json;

namespace RMM.Data.Model
{
    [Table(Name="Option")]
    [JsonObject]
    public class Option
    {
         //IsDbGenerated = true   1 SEUL FICHIER D'OPTION
        [Column(IsPrimaryKey = true)]
        [JsonProperty]
        public int id { get; set; }

        [Column]
        [JsonProperty]
        public bool IsPassword { get; set; }

        [Column]
        [JsonProperty]
        public bool IsSynchro { get; set; }

        [Column]
        [JsonProperty]
        public DateTime RefreshTimeBackup { get; set; }

        [Column]
        [JsonProperty]
        public int LastBackupVersion { get; set; }

        [Column]
        [JsonProperty]
        public bool IsPrimaryTile { get; set; }


        [Column]
        [JsonProperty]
        public int Favorite { get; set; }

        [Column]
        [JsonProperty]
        public DateTime ModifiedDate { get; set; }
    }
}
