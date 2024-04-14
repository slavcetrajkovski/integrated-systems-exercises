namespace MovieApp.Models.DTO
{
    public class ShoppingCartDTO
    {
        public List<TicketsInShoppingCart>? inShoppingCart {  get; set; }
        public double TotalPrice { get; set; }
    }
}
