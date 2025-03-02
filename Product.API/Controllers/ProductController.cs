using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.API.Reply;
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
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery]GetAllProductsRequest allProductsRequest)
        {
            if (allProductsRequest.IsValid())
            {
                var response = 
                    await _mediator.Send(
                        new ProductGetAllQuery(
                            allProductsRequest.OrderedByName, 
                            allProductsRequest.OrderedByShape, 
                            allProductsRequest.FilteredByShape, 
                            allProductsRequest.FilteredByLegalStatus));
                
                var pagedUsers = PagedList.Create(response, allProductsRequest.PageNumber, allProductsRequest.PageSize);
        
                return Ok(pagedUsers);
            }

            return BadRequest();
        }

        [HttpGet("{productDIN}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]

        public async Task<IActionResult> GetAsync(string productDIN)
        {
            var response = await _mediator.Send(new ProductGetQuery(productDIN));
            
            if (response is null)
                return NotFound(productDIN);

            return Ok(new ProductReply(response));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> AddProductAsync([FromQuery]InsertOrUpdateProductRequest insertRequest)
        {
            var productExist = await _mediator.Send(new ProductGetQuery(insertRequest.DIN));
            if (productExist != null)
                return new StatusCodeResult(StatusCodes.Status302Found);

            var response = await _mediator.Send(new ProductAddOrUpdateCommand(insertRequest.ToDomain()));
            
            if (!string.IsNullOrEmpty(response))
                return new StatusCodeResult(StatusCodes.Status201Created);
            
            return BadRequest();
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductAsync([FromQuery]InsertOrUpdateProductRequest updateRequest)
        {
            var productExist = await _mediator.Send(new ProductGetQuery(updateRequest.DIN));
            if (productExist == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            var response = await _mediator.Send(new ProductAddOrUpdateCommand(updateRequest.ToDomain()));
            
            if (!string.IsNullOrEmpty(response))
                return Accepted();
            
            return BadRequest(response);
        }
    }
}