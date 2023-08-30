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
    [Route("api/collection")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ILogger<CollectionController> _logger;
        private readonly IMapper _mapper;
        private readonly ICollectionService _collectionService;
        private const string controllerPrefix = "Collection";
        private const int maxPageSize = 20;

        public CollectionController(ILogger<CollectionController> logger, IMapper mapper, ICollectionService collectionService)
        {
            _logger = logger;
            _mapper = mapper;
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectionsAsync([FromQuery] PaginationRequestModel pagination)
        {
            try
            {
                if (pagination.pageSize > maxPageSize)
                {
                    pagination.pageSize = maxPageSize;
                }

                var (collections, paginationMetadata) = await _collectionService.GetCollectionsAsync(pagination);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

                var collectionsToReturn = _mapper.Map<IEnumerable<CollectionResponseModel>>(collections);

                return Ok(collectionsToReturn);
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
        public async Task<IActionResult> GetAllCollectionsAsync()
        {
            try
            {
                var collections = await _collectionService.GetAllCollectionAsync();
                
                var collectionsToReturn = _mapper.Map<IEnumerable<CollectionResponseModel>>(collections);
                
                return Ok(collectionsToReturn);
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
        public async Task<IActionResult> GetCollectionByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var collection = await _collectionService.GetCollectionByIdAsync(id);

                var collectionToReturn = _mapper.Map<CollectionResponseModel>(collection);

                return Ok(collectionToReturn);
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
        public async Task<IActionResult> CreateCollectionAsync([FromBody] CollectionRequestModel requestModel)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var collection = await _collectionService.CreateCollectionAsync(requestModel);

                var collectionReturn = _mapper.Map<CollectionResponseModel>(collection);

                return Created($"collection/{collectionReturn.Id}", collectionReturn);
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
        public async Task<IActionResult> UpdateCollectionAsync([FromBody] CollectionRequestModel requestModel, [FromRoute] Guid id)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var collection = await _collectionService.UpdateCollectionAsync(requestModel, id);

                var collectionToReturn = _mapper.Map<CollectionResponseModel>(collection);

                return Ok(collectionToReturn);
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
        public async Task<IActionResult> DeleteCollectionAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _collectionService.DeleteCollectionAsync(id);

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
