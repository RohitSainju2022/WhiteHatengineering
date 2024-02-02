using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Common.ServiceResponse;
using WhiteHat.Common.Static.Enum;
using WhiteHat.Data.WhiteHatDbContext;

namespace WhiteHat.MediatR.MediatRService.UserService
{
    public record DeleteUserCommand(Guid UserId) : IRequest<ServiceResponse<bool>>;

    public record DeleteUserCommandHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMediator Mediator) :
        IRequestHandler<DeleteUserCommand, ServiceResponse<bool>>
    {
        public async Task<ServiceResponse<bool>> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<bool> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfo = await dbContext.UserInfoes
                    .Where(u => u.UserId == request.UserId && !u.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbUserInfo != null)
                {
                    dbUserInfo.IsDeleted = true;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = true;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        //response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = (HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }
                else
                {
                    //errors.Add(Res.ViewResources.NotFound);
                    response.Result = false;
                    response.Type = ServiceResponseTypes.ERROR;
                    response.ErrorCode = (HttpStatusCode.InternalServerError).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
