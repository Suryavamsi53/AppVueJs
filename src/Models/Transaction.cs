using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.src.Models
{
    public class Transaction
    {
        public int TransID { get; set; }
        public int AccountID { get; set; }
        public int TransTypeID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Transaction_type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}