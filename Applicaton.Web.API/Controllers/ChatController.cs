﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Applicaton.Web.API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
	[Route("api/chat")]
	[Authorize]
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

		[Authorize(Policy = "UserRight")]
		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<ChatResponseModel>>> GetAllMessageByChatIdAsync([FromQuery] PaginationRequestModel pagination, [FromRoute] Guid userId)
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
		/// Acquire messages information with pagination via chat identification
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="200">Successfully get items information.</response>
		/// <response code="500">There is something wrong while execute.</response>
		
		[Authorize(Policy = "UserRight")]
		[HttpGet("message/{chatId}")]
		public async Task<ActionResult<MessageResponseModel>> GetAllChatsByUserIdAsync([FromQuery] PaginationRequestModel pagination, [FromRoute] Guid chatId)
		{
			try
			{
				if (pagination.pageSize > maxPageSize)
				{
					pagination.pageSize = maxPageSize;
				}

				var (messages, paginationMetadata) = await _chatService.GetAllMessagesByChatId(pagination, chatId);

				//Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
				Response.AddPaginationHeader(paginationMetadata);

				var messagesToReturn = _mapper.Map<IEnumerable<MessageResponseModel>>(messages);

				return Ok(messagesToReturn);
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
		/// Create a message
		/// </summary>
		/// <returns>Status code of the action.</returns>
		/// <response code="201">Successfully created item.</response>
		/// <response code="500">There is something wrong while execute.</response>
		
		[Authorize(Policy = "UserRight")]
		[HttpPost("message/{chatId}")]
		public async Task<ActionResult<MessageResponseModel>> CreateMessageAsync([FromBody] MessageRequestModel requestModel, [FromRoute] Guid chatId)
		{
			try
			{
				var message = await _chatService.CreateMessageAsync(requestModel, chatId);

				var messageToReturn = _mapper.Map<MessageResponseModel>(message);

				return Ok(messageToReturn);
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
		
		[Authorize(Policy = "UserRight")]
		[HttpPost]
		public async Task<ActionResult<ChatResponseModel>> CreateChatAsync([FromBody] ChatRequestModel requestModel)
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
