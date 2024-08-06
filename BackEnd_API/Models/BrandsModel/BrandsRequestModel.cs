namespace Models
{
    public class BrandsRequestModel
    {
        public string OPERATION_TYPE { get; set; } = string.Empty;
        public string VSL_OBJECT_ID { get; set; } = string.Empty;
        public string VSL_CATEGORY_ID { get; set; } = string.Empty;
        public List<SPBRANDNAMESV5TYPMODEL>? P_DATA { get; set; }
        public int? USER_ID { get; set; }
        public int? OFFSET { get; set; }
        public int? NEXT { get; set; }
        public string ORDERBYEXP { get; set; } = string.Empty;
        public string WHEREEXP { get; set; } = string.Empty;
        public int? TOTAL_COUNT { get; set; }
        public string STATUS { get; set; } = string.Empty;
        public string MESSAGE { get; set; } = string.Empty;
    }
}