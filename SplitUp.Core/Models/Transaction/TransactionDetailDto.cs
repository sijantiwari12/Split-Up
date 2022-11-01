using SplitUp.Core.Models.Credit;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitUp.Core.Models.Transaction
{
    public class TransactionDetailDto
    {
        public int TransactionId { get; set; }

        public String TransactionName { get; set; }

        public int NoOfIndividuals { get; set; }

        public double TotalAmountPaid { get; set; }

        public IEnumerable<CreditorsPageDto> Creditors { get; set; }
    }
}
