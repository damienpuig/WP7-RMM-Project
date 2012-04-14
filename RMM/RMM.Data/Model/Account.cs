using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RMM.Data.Model
{
    [Table(Name="Account")]
    [JsonObject]
    public class Account
    {

        private EntitySet<Transaction> transactionRef;

        public Account()
        {
            this.transactionRef = new EntitySet<Transaction>(this.OnTransactionAdded, this.OnTransactionRemoved);
            
        }


        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        [JsonProperty]
        public int ID { get; set; }

        [Column]
        [JsonProperty]
        public string Name { get; set; }

        [Column]
        [JsonProperty]
        public double Balance { get; set; }
       
        [Column]
        [JsonProperty]
        public string BankName { get; set; }

        [Association(Name = "FK_Account_Transactions", Storage = "transactionRef", ThisKey = "ID", OtherKey = "AccountID")]
        [JsonIgnore]
        public EntitySet<Transaction> TransactionList 
        {
            get { return this.transactionRef; }
        }



        private void OnTransactionAdded(Transaction transaction)
        {
            transaction.Account = this;
        }

        private void OnTransactionRemoved(Transaction transaction)
        {
            transaction.Account = this;
        } 
        
        [Column]
        [JsonProperty]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime CreatedDate { get; set; }
    }
}
