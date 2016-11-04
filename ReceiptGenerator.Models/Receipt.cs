using System.Collections.Generic;

namespace ReceiptGenerator.Models
{
    public class Receipt
    {
        public IEnumerable<ReceiptLine> Lines { get; set; }

        public decimal Total { get; set; }

        public decimal SalesTaxes { get; set; }
    }
}
