using System.ComponentModel.DataAnnotations;

namespace ProductService.Data.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Descriptions { get; set; }
        public string Version { get; set; }
    }
}
