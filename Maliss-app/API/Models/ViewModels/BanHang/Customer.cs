namespace API.Models.ViewModels.BanHang
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Type { get; set; }
        public bool Active { get; set; } = true;
    }
}
