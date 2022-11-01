using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

public class Credit
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Transaction")]
    public int TransactionId { get; set; }

    public Transaction Transaction { get; set; }

    //[ForeignKey("User")]
    //public int PayerId { get; set; }

    //public User Payer { get; set; }

    [ForeignKey("User")]
    public int CreditorId { get; set; }

    public User Creditor { get; set; }

    public double AmountToPay { get; set; }

    public int Status { get; set; }

    public int Ping { get; set; }

    public DateTime? UpdatedDate { get; set; }

}
