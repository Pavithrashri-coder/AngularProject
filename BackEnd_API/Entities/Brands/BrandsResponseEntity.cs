namespace Entities
{
    public class BrandsResponseEntity
    {
        public int ID { get; set; }
        public string NAME { get; set; } = string.Empty;
        public string CODE { get; set; } = string.Empty;
        public int CATEGORY_ID { get; set; }
        public string ACTIVE { get; set; } = string.Empty;
    }
}
