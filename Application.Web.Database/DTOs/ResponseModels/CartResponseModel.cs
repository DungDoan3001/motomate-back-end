﻿namespace Application.Web.Database.DTOs.ResponseModels
{
	public class CartResponseModel
	{
		public Guid UserId { get; set; }
		public List<ShopOfCart> Shops { get; set; }
	}

	public class ShopOfCart
	{
		public Guid LessorId { get; set; }
		public string LessorName { get; set; }
		public List<VehicleOfLessor> Vehicles { get; set; }
	}

	public class VehicleOfLessor
	{
		public Guid VehicleId { get; set; }
		public string VehicleName { get; set;}
	}
}