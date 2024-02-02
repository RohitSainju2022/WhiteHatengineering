using AutoMapper;
using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Common.ServiceResponse;
using WhiteHat.Common.Static;
using WhiteHat.Common.Static.Constant;
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Models;
using WhiteHat.Ui.Models.Models;
using WhiteHat.Common.Mapper.CustomMapper;
using WhiteHat.Common.Static.Enum;
using System.Net;

namespace WhiteHat.MediatR.MediatRService.UserService
{
    public record InsertUserCommand(UserInfoModel UserInfoModel): IRequest<ServiceResponse<UserInfoModel>>;
    public record InsertUserCommandHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertUserCommand, ServiceResponse<UserInfoModel>>
    {
        public async Task<ServiceResponse<UserInfoModel>> Handle(InsertUserCommand request ,CancellationToken cancellationToken)
        {
                ServiceResponse<UserInfoModel> response = new();
                List<string> errors = new();
            if (request.UserInfoModel != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbUser = UserCustomMapper.ToEntity(new UserInfo(),request.UserInfoModel);
                    dbContext.UserInfoes.Add(dbUser);
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                //errors.Add(Res.ErrorMessage.InvalidInput);
                response.Type = ServiceResponseTypes.BADPARAMETERS;
                response.ErrorCode = ((int)HttpStatusCode.BadRequest).ToString();
                response.Errors = errors;
            }

            return response;
        }
    }
}
