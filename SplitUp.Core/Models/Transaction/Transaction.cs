using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }

        public double AmountPaid { get; set; }

        public string BillName { get; set; }

        public int NoOfIndividuals { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Memo { get; set; }
        
        [NotMapped]
        public IEnumerable<Credit> Creditors { get; set; }
    }

