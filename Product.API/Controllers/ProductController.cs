using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.API.Request;
using Product.Application.Commands;
using Product.Application.Queries;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetAllProductsRequest allProductsRequest)
        {
            var response = await _mediator.Send(new ProductGetAllQuery());
            
            if (!string.IsNullOrEmpty(response))
                return Ok(response);
            
            return BadRequest(response);
        }

        [HttpGet("{productDIN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAsync(string productDIN)
        {
            var response = await _mediator.Send(new ProductGetQuery(productDIN));
            
            if (response is null)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> AddProductAsync([FromQuery]InsertOrUpdateProductRequest insertRequest)
        {
            var response = await _mediator.Send(new ProductAddOrUpdateCommand(insertRequest.ToDomain()));
            
            if (!string.IsNullOrEmpty(response))
                return Created();
            
            return BadRequest();
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductAsync([FromQuery]InsertOrUpdateProductRequest updateRequest)
        {
            var response = await _mediator.Send(new ProductAddOrUpdateCommand(updateRequest.ToDomain()));
            
            if (!string.IsNullOrEmpty(response))
                return Accepted();
            
            return BadRequest(response);
        }
    }
}