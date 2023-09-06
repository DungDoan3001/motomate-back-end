using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Applicaton.Web.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private const string controllerPrefix = "Product";
        private const int maxPageSize = 20;

        public ProductController(ILogger<ProductController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
