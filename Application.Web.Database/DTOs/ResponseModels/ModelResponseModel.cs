﻿namespace Application.Web.Database.DTOs.ResponseModels
{
    public class ModelResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Capacity { get; set; }
        public CollectionOfModel Collection { get; set; }
        public List<ColorOfModel> Colors { get; set; }
    }

    public class CollectionOfModel
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ColorOfModel
    {
        public Guid Id { get; set; }
        public string Color { get; set; }
    }
}