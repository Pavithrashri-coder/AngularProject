using Models;

namespace BusinessLayer
{
    public interface IBrandsService
    {
        Task<List<BrandsResponseModel>> GetBrands(BrandsRequestModel request);
        Task<List<BrandsResponseModel>> GetRequistionCategory(BrandsRequestModel request);
    }
}
