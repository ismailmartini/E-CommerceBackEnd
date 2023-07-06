using E_CommerceBackEnd.Domain.Entities.Common;
 
namespace E_CommerceBackEnd.Domain.Entities
{
    public class BasketItem:BaseEntities
    {
        public Guid BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Basket Basket { get; set; }
        public Product Product { get; set; }

      

    }
}
