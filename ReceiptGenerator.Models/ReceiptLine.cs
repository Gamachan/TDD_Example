namespace ReceiptGenerator.Models
{
    public class ReceiptLine
    {
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
    }
}