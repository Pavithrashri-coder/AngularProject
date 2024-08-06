using Entities;


namespace DataAccessLayer
{
    public interface IBrandsRepository
    {
        Task<List<BrandsResponseEntity>> GetBrands(BrandsRequestEntity request);
        Task<List<BrandsResponseEntity>> GetRequistionCategory(BrandsRequestEntity request);
    }
}
