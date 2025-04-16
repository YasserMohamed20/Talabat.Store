using System.ComponentModel.DataAnnotations;

namespace Talabat.Store.Dto
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> items { get; set; }

    }
}
