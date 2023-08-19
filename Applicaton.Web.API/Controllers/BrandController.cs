using System.Text.Json;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly ILogger<BrandController> _logger;
        private readonly IMapper _mapper;
        private const string controllerPrefix = "Brand";
        private const int maxPageSize = 20;

        public BrandController(ILogger<BrandController> logger, IMapper mapper, IBrandService brandService)
        {
            _brandService = brandService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandsAsync([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                if (pagination.pageSize > maxPageSize)
                {
                    pagination.pageSize = maxPageSize;
                }

                var (brands, paginationMetadata) = await _brandService.GetBrandsAsync(pagination);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                var brandsToReturn = _mapper.Map<IEnumerable<BrandResponseModel>>(brands);

                return Ok(brandsToReturn);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBrandsAsync()
        {
            try
            {
                var brandToReturn = await _brandService.GetAllBrandsAsync();

                return Ok(brandToReturn);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _brandService.GetBrandByIdAsync(id);

                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<BrandResponseModel>(result));
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrandAsync([FromBody] BrandRequestModel requestModel)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var result = await _brandService.CreateBrandAsync(requestModel);

                return Created($"brand/{result.Id}", result);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBrandAsync([FromBody] BrandRequestModel requestModel, [FromRoute] Guid id)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var result = await _brandService.UpdateBrandAsync(requestModel, id);

                return Ok(result);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _brandService.DeleteBrandAsync(id);

                if (!result)
                    throw new StatusCodeException(message: "Error hit.", statusCode: StatusCodes.Status500InternalServerError);
                else
                    return NoContent();
            }
            catch (StatusCodeException ex)
            {
                return StatusCode(ex.StatusCode, new ErrorResponseModel
                {
                    Message = ex.Message,
                    StatusCode = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{controllerPrefix} error at {Helpers.GetCallerName()}: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseModel
                {
                    Message = "Error while performing action.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Errors = { ex.Message }
                });
            }
        }
    }
}
