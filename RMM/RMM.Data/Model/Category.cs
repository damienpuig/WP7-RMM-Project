using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using Newtonsoft.Json;

namespace RMM.Data.Model
{
    [Table(Name="Category")]
    [JsonObject]
    public class Category
    {

        private EntitySet<Transaction> transactionRef;

        public Category()
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
        public string Color { get; set; }


        [Association(Name = "FK_Category_Transaction", Storage = "transactionRef", ThisKey = "ID", OtherKey = "CategoryID")]
        [JsonIgnore]
        public EntitySet<Transaction> TransactionList
        {
            get { return this.transactionRef; }
        }


        private void OnTransactionAdded(Transaction transaction)
        {
            transaction.Category = this;
        }

        private void OnTransactionRemoved(Transaction transaction)
        {
            transaction.Category = this;
        } 
        

        [Column]
        [JsonProperty]
        public DateTime CreatedDate { get; set; }
    }
}
