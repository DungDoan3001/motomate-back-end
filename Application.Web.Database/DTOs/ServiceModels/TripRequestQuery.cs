﻿using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ServiceModels
{
	public class TripRequestQuery
	{
		[JsonPropertyName("lessorUsername")]
		public string? LessorUsername { get; set; } = "";

		[JsonPropertyName("lesseeUsername")]
		public string? LesseeUsername { get; set; } = "";

		[JsonPropertyName("searchQuery")]
		public string? SearchQuery { get; set; } = "";

		[JsonPropertyName("status")]
		public string? Status { get; set; } = "";
	}
}
