using System;
using System.Collections.Generic;
using System.Text;

namespace SplitUp.Core.Models.Credit
{
    public class CreditorsPageDto
    {
        public int UserId { get; set; }

        public int FullName { get; set; }

        public double AmountToPay { get; set; }

        public int Status { get; set; }
    }
}
