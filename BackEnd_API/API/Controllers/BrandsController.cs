using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService _brandService;

        public BrandsController(IBrandsService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBrands(BrandsRequestModel request)
        {
            try
            {
                var result = await _brandService.GetBrands(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRequistionCategory(BrandsRequestModel request)
        {
            try
            {
                var result = await _brandService.GetRequistionCategory(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
