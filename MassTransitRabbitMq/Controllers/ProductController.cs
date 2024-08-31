using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace MassTransitRabbitMq.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IPublishEndpoint _publish;
        public ProductController(IPublishEndpoint publish)
        {
            _publish = publish;

        }


        [HttpGet]
        [Route("all")]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        [Route("add")]
        public Task Post([FromBody] ProductCreateDto product)
        {
            _publish.Publish<ProductMessage>(new
            {
                Code = Guid.NewGuid(),
                Name = product.Name
            });
            return Task.CompletedTask;
        }
    }
}
