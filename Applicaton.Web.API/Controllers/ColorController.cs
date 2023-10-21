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
    [Route("api/color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly ILogger<ColorController> _logger;
        private readonly IMapper _mapper;
        private const string controllerPrefix = "Color";
        private const int maxPageSize = 20;

        public ColorController(ILogger<ColorController> logger, IMapper mapper, IColorService colorService)
        {
            _colorService = colorService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Acquire all colors information
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ColorResponseModel>>> GetAllColorsAsync()
        {
            try
            {
                var colors = await _colorService.GetColorsAsync();

                var colorsToReturn = _mapper.Map<IEnumerable<ColorResponseModel>>(colors);

                return Ok(colorsToReturn);
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

        /// <summary>
        /// Acquire color information by identification.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully get item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorResponseModel>> GetColorAsync([FromRoute] Guid id)
        {
            try
            {
                var color = await _colorService.GetColorByIdAsync(id);

                if (color == null)
                    return NotFound();

                var colorToReturn = _mapper.Map<ColorResponseModel>(color);

                return Ok(colorToReturn);
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

        /// <summary>
        /// Create color
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="201">Successfully created item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost]
        public async Task<ActionResult<ColorResponseModel>> CreateColorAsync([FromBody] ColorRequestModel requestModel)
        {
            try
            {
                if (requestModel.Color.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var color = await _colorService.CreateColorAsync(requestModel);

                var colorToReturn = _mapper.Map<ColorResponseModel>(color);

                return Created($"color/{colorToReturn.Id}", colorToReturn);
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

        /// <summary>
        /// Create bulk colors.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully created items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<ColorResponseModel>>> CreateBulkColorsAsync([FromBody] List<ColorRequestModel> requestModels)
        {
            try
            {
                if (requestModels.Any(x => x.Color.IsNullOrEmpty()))
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var (createdColors, alreadyExistedColors, errorWhenCreatingColors) = await _colorService.CreateBulkColorsAsync(requestModels);

                var colorToReturn = _mapper.Map<IEnumerable<ColorResponseModel>>(createdColors);

                var objectToReturn = new
                {
                    created = colorToReturn,
                    alreadyExisted = alreadyExistedColors,
                    errorWhenCreating = errorWhenCreatingColors
                };

                return Ok(objectToReturn);
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

        /// <summary>
        /// Update color information by identification.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="200">Successfully udpated item information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<ColorResponseModel>> UpdateColorAsync([FromBody] ColorRequestModel requestModel, [FromRoute] Guid id)
        {
            try
            {
                if (requestModel.Color.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var color = await _colorService.UpdateColorAsync(requestModel, id);

                var colorToReturn = _mapper.Map<ColorResponseModel>(color);

                return Ok(colorToReturn);
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

        /// <summary>
        /// Delete color information by identification.
        /// </summary>
        /// <returns>Status code of the action.</returns>
        /// <response code="204">Successfully deleted items information.</response>
        /// <response code="500">There is something wrong while execute.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColorhAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _colorService.DeleteColorAsync(id);

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
