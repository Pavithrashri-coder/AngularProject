using Entities;
using Options;

namespace DataAccessLayer
{
    public class BrandsRepository : IBrandsRepository
    {
        private readonly IDbContext _dbContext;

        public BrandsRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BrandsResponseEntity>> GetBrands(BrandsRequestEntity request)
        {
            try
            {
                var result = await _dbContext.ExecuteListAsync<BrandsRequestEntity, BrandsResponseEntity>(StoredProcedure.GetV4BrandNames, request);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<List<BrandsResponseEntity>> GetRequistionCategory(BrandsRequestEntity request)
        {
            try
            {
                var result = await _dbContext.ExecuteListAsync<BrandsRequestEntity, BrandsResponseEntity>(StoredProcedure.GetV4BrandNames, request);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}