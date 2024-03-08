namespace MagicVilla_API.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Details { get; set; }
        public double Product_Rate { get; set; }
        public string ImageUrl { get; set; }
    }
}
