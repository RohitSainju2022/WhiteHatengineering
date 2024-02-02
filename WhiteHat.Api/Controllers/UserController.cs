using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics;
using WhiteHat.Api.Controllers.Base;
using WhiteHat.MediatR.MediatRService.UserService;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
                
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _mediator.Send(new GetUsersQuery());
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult>Insertasync(UserInfoModel model)
        {
            if(ModelState.IsValid)
            {
                var response = await _mediator.Send(new InsertUserCommand(model));
                if (response != null) 
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Updateasync([FromBody]UserInfoModel model, Guid id)
        { 
            if (ModelState.IsValid) 
            {
                var response = await _mediator.Send(new UpdateUserCommand(id,model));
                if (response != null)
                {
                    return Ok(response);
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteasync(Guid id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
