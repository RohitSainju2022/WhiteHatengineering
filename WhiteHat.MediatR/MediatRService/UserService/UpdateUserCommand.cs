using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Common.Mapper.CustomMapper;
using WhiteHat.Common.ServiceResponse;
using WhiteHat.Common.Static.Constant;
using WhiteHat.Common.Static.Enum;
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.MediatR.MediatRService.UserService
{
    public record UpdateUserCommand(Guid UserId,UserInfoModel UserInfoModel): IRequest<ServiceResponse<UserInfoModel>>;

    public record UpdateUserCommandHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper , IMediator Mediator)
        :IRequestHandler<UpdateUserCommand,ServiceResponse<UserInfoModel>>
    {
        public async Task<ServiceResponse<UserInfoModel>> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<UserInfoModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfo = await dbContext.UserInfoes
                    .Where(u => u.UserId == request.UserId && !u.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbUserInfo != null)
                {
                    Mapper.Map(request.UserInfoModel, dbUserInfo);
                    dbContext.Entry(dbUserInfo).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.UserInfoModel;
                        response.Type = ServiceResponseTypes.SUCCESS;
                    }
                    else
                    {
                        //errors.Add(UserConstant.User_Update_Error);
                        response.Type = ServiceResponseTypes.ERROR;
                        response.ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Errors = errors;
                    }
                }

                else
                {
                    errors.Add(UserConstant.User_NotFound);
                    response.Type = ServiceResponseTypes.NOTFOUND;
                    response.ErrorCode = ((int)HttpStatusCode.NotFound).ToString();
                    response.Errors = errors;
                }
            }
            return response;

        }
           
    }
}
