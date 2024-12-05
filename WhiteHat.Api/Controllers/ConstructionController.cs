using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhiteHat.Api.Controllers.Base;
using WhiteHat.MediatR.MediatRService.ConstructionService;
using WhiteHat.MediatR.MediatRService.UserService;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ConstructionController : BaseController
    {
        private readonly IMediator _mediator;

        public ConstructionController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetConstructionsQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var response = await _mediator.Send(new GetConstructionByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult> Insertasync(ConstructionModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new InsertConstructionCommand(model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Updateasync([FromBody] ConstructionModel model, int id)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(new UpdateConstructionCommand(id, model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteasync(int id)
        {
            var response = await _mediator.Send(new DeleteConstructionCommand(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
