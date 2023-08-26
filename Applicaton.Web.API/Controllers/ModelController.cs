﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/model")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly ILogger<ModelController> _logger;
        private readonly IMapper _mapper;
        private const string controllerPrefix = "Model";
        private const int maxPageSize = 20;

        public ModelController(ILogger<ModelController> logger, IMapper mapper, IModelService modelService)
        {
            _modelService = modelService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllModelsAsync()
        {
            try
            {
                var models = await _modelService.GetAllModelsAsync();

                var modelsToReturn = _mapper.Map<IEnumerable<ModelResponseModel>>(models);

                return Ok(modelsToReturn);
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
        public async Task<IActionResult> GetModelByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var model = await _modelService.GetModelByIdAsync(id);

                var modelToReturn = _mapper.Map<ModelResponseModel>(model);

                return Ok(modelToReturn);
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
        public async Task<IActionResult> CreateModelAsync([FromBody] ModelRequestModel requestModel)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var model = await _modelService.CreateModelAsync(requestModel);

                var modelToReturn = _mapper.Map<ModelResponseModel>(model);

                return Created($"model/{modelToReturn.Id}", modelToReturn);
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
        public async Task<IActionResult> UpdateModelAsync([FromBody] ModelRequestModel requestModel, [FromRoute] Guid id)
        {
            try
            {
                if (requestModel.Name.IsNullOrEmpty())
                    throw new StatusCodeException(message: "Invalid request.", statusCode: StatusCodes.Status400BadRequest);

                var model = await _modelService.UpdateModelAsync(requestModel, id);

                var modelToReturn = _mapper.Map<ModelResponseModel>(model);

                return Ok(modelToReturn);
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
        public async Task<IActionResult> DeleteModelAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _modelService.DeleteModelAsync(id);

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