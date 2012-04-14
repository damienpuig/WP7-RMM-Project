using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using Newtonsoft.Json;


namespace RMM.Data.Model
{
    [Table(Name="Transaction")]
    [JsonObject]
    public class Transaction
    {
         [JsonIgnore]
        private Nullable<int> accountID;
        [JsonIgnore]
        private Nullable<int> categoryID;

        private EntityRef<Account> AccountRef = new EntityRef<Account>();
        private EntityRef<Category> CategoryRef = new EntityRef<Category>();


        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        [JsonProperty]
        public int ID { get; set; }

        [Column]
        [JsonProperty]
        public string Name { get; set; }

        [Column]
        [JsonProperty]
        public string Description { get; set; }

        [Column]
        [JsonProperty]
        public double Amount { get; set; }


    [Column(Storage="accountID", DbType="Int")]
        [JsonProperty]
        public int? AccountID
    {
        get { return this.accountID;}
        set { this.accountID = value; }
    }

       [Column(Storage="categoryID", DbType="Int")]
        [JsonProperty]
        public int? CategoryID
    {
        get { return this.categoryID;}
        set { this.categoryID = value; }
    }



        [Association(Name="FK_Account_Transactions", Storage = "AccountRef", ThisKey = "AccountID", OtherKey = "ID", IsForeignKey = true)]
        [JsonIgnore]
        public Account Account 
        {
            get { return this.AccountRef.Entity; }
            set 
            {
                Account previousValue = this.AccountRef.Entity; 
                 
                 if (previousValue != value || this.AccountRef.HasLoadedOrAssignedValue == false)
                 {
                     if (previousValue != null)
                     {
                         this.AccountRef.Entity = null; 
                         previousValue.TransactionList.Remove(this);
                     } 
                     
                     this.AccountRef.Entity = value; 
                     
                     if (value != null)
                     {
                         value.TransactionList.Add(this);
                         this.accountID = value.ID;
                     } 
                     else
                     {
                         this.AccountID = default(Nullable<int>);
                     }
                 }
            }
        }

        
        [Association(Name = "FK_Category_Transactions", Storage = "CategoryRef", ThisKey = "CategoryID", OtherKey = "ID", IsForeignKey = true)]
        [JsonIgnore]
        public Category Category 
        {
            get { return this.CategoryRef.Entity; }
            set
            {
                Category previousValue = this.CategoryRef.Entity;

                if (previousValue != value || this.CategoryRef.HasLoadedOrAssignedValue == false)
                {
                    if (previousValue != null)
                    {
                        this.CategoryRef.Entity = null;
                        previousValue.TransactionList.Remove(this);
                    }

                    this.CategoryRef.Entity = value;

                    if (value != null)
                    {
                        value.TransactionList.Add(this);
                        this.categoryID = value.ID;
                    }
                    else
                    {
                        this.categoryID = default(Nullable<int>);
                    }
                }
            }
        }

        [Column]
        [JsonProperty]
        public DateTime CreatedDate { get; set; }
    }
}
