using DataAccessLayer;
using Entities;
using Microsoft.Extensions.Logging;
using Models;
using Options;

namespace BusinessLayer
{
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsRepository _brandsRepository;
        private readonly IAutoMapperService _autoMapper;
        private readonly ILogger<BrandsService> _logger;

        public BrandsService(IBrandsRepository brandsRepository, IAutoMapperService autoMapper, ILogger<BrandsService> logger)
        {
            _brandsRepository = brandsRepository;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        public async Task<List<BrandsResponseModel>> GetBrands(BrandsRequestModel request)
        {
            try
            {
                var req = new BrandsRequestEntity
                {
                    OPERATION_TYPE = request.OPERATION_TYPE,
                    VSL_OBJECT_ID = request.VSL_OBJECT_ID,
                    MESSAGE = request.MESSAGE,
                    NEXT = request.NEXT,
                    OFFSET = request.OFFSET,
                    ORDERBYEXP = request.ORDERBYEXP,
                    P_DATA = _autoMapper.AutoMapping<SPBRANDNAMESV5TYPMODEL, SPBRANDNAMESV5TYPENTITY>(request.P_DATA!),
                    STATUS = request.STATUS,
                    TOTAL_COUNT = request.TOTAL_COUNT,
                    USER_ID = request.USER_ID,
                    VSL_CATEGORY_ID = request.VSL_CATEGORY_ID,
                    WHEREEXP = request.WHEREEXP
                };
                var response = await _brandsRepository.GetBrands(req);
                var mappedResponse = _autoMapper.AutoMapping<BrandsResponseEntity, BrandsResponseModel>(response);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }

        public async Task<List<BrandsResponseModel>> GetRequistionCategory(BrandsRequestModel request)
        {
            try
            {
                var req = new BrandsRequestEntity
                {
                    OPERATION_TYPE = request.OPERATION_TYPE,
                    VSL_OBJECT_ID = request.VSL_OBJECT_ID,
                    MESSAGE = request.MESSAGE,
                    NEXT = request.NEXT,
                    OFFSET = request.OFFSET,
                    ORDERBYEXP = request.ORDERBYEXP,
                    P_DATA = _autoMapper.AutoMapping<SPBRANDNAMESV5TYPMODEL, SPBRANDNAMESV5TYPENTITY>(request.P_DATA!),
                    STATUS = request.STATUS,
                    TOTAL_COUNT = request.TOTAL_COUNT,
                    USER_ID = request.USER_ID,
                    VSL_CATEGORY_ID = request.VSL_CATEGORY_ID,
                    WHEREEXP = request.WHEREEXP
                };
                var response = await _brandsRepository.GetRequistionCategory(req);
                var mappedResponse = _autoMapper.AutoMapping<BrandsResponseEntity, BrandsResponseModel>(response);
                return mappedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }
    }
}