using System.Security.Claims;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Web.Service.Helpers
{
	public static class IdentityHelpers
	{
		public static IdentityClaimsValue GetCurrentLoginUserClaims(ClaimsIdentity identity)
		{
			bool isIdentityHaveClaims = identity.Claims.Any();

			if (!isIdentityHaveClaims)
				throw new StatusCodeException(message: "User not authenticated.", statusCode: StatusCodes.Status403Forbidden);

			var identityEmail = identity.FindFirst(ClaimTypes.Email);

			var identityUsername = identity.FindFirst(ClaimTypes.NameIdentifier);

			var identityRoles = identity.FindAll(ClaimTypes.Role);

			if (identityEmail.Value.IsNullOrEmpty())
				throw new StatusCodeException(message: "Missing user information.", statusCode: StatusCodes.Status400BadRequest);

			IdentityClaimsValue claimValues = new()
			{
				IdentityEmail = identityEmail.Value,
				IdentityUsername = identityUsername.Value,
				IdentityRoles = identityRoles.Select(x => x.Value).ToList(),
			};

			return claimValues;
		}
	}
}
