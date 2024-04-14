namespace MovieApp.Models.DTO
{
    public class AddTicketToCartDTO
    {
        public Guid TicketId { get; set; }
        public string? SelectedMovie { get; set; }
        public int Quantity { get; set; }
    }
}
