﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Application.Web.Service.Services;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/chat")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ILogger<ChatController> _logger;
		private readonly IChatService _chatService;
		private const string controllerPrefix = "Chat";
		private const int maxPageSize = 20;

		public ChatController(ILogger<ChatController> logger, IMapper mapper, IChatService chatService)
        {
			_logger = logger;
			_mapper = mapper;
            _chatService = chatService;
        }

		/// <summary>
		/// Acquire chats information with pagination
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpGet("{userId}")]
		public async Task<IActionResult> GetAllChatsByUserIdAsync([FromQuery] PaginationRequestModel pagination, [FromRoute] Guid userId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (chats, paginationMetadata) = await _chatService.GetAllChatsByUserAsync(pagination, userId);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var chatsToReturn = _mapper.Map<IEnumerable<ChatResponseModel>>(chats);

				return Ok(chatsToReturn);
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
		/// Create a chat
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		[HttpPost]
		public async Task<IActionResult> CreateChatAsync([FromBody] ChatRequestModel requestModel)
		{
			try
			{
				var chat = await _chatService.CreateChatAsync(requestModel);

				var chatToReturn = _mapper.Map<ChatResponseModel>(chat);

				return Ok(chatToReturn);
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