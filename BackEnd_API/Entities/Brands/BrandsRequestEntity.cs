using Options.Attributes;
using System.Data;
namespace Entities
{
    public class BrandsRequestEntity
    {
        public string OPERATION_TYPE { get; set; } = string.Empty;
        public string VSL_OBJECT_ID { get; set; } = string.Empty;
        public string VSL_CATEGORY_ID { get; set; } = string.Empty;
        [DBParameter(ParameterType = "[Purchase].[SP_BRAND_NAMES_V4_TYP]")]
        public List<SPBRANDNAMESV5TYPENTITY>? P_DATA { get; set; }
        public int? USER_ID { get; set; }
        public int? OFFSET { get; set; }      
        public int? NEXT { get; set; }
        public string ORDERBYEXP { get; set; } = string.Empty;
        public string WHEREEXP { get; set; } = string.Empty;

        [DBParameter(ParameterDirection = ParameterDirection.Output)]
        public int? TOTAL_COUNT { get; set; }
        [DBParameter(ParameterDirection = ParameterDirection.Output)]
        public string STATUS { get; set; } = string.Empty;
        [DBParameter(ParameterDirection = ParameterDirection.Output)]
        public string MESSAGE { get; set; } = string.Empty;
    }
}