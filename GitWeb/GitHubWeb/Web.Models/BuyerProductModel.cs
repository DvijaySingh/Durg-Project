namespace Web.Models
{
    public class BuyerProductModel
    {
        public long BuyerProductlD { get; set; }
        public long? BuyerID { get; set; }
        public string ProductName { get; set; }
        public  decimal? Weight { get; set; }
        public  int? Units { get; set; }
        public string Type { get; set; }
        public  string CategoryID { get; set; }
    }
}
