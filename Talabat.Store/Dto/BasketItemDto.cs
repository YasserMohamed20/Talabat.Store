using System.ComponentModel.DataAnnotations;

namespace Talabat.Store.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Barnd { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "Price Can Not Be Zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be one Item At least")]
        public int Quantity { get; set; }
    }
}
