using AutoMapper;
using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WhiteHat.Common.ServiceResponse;
using WhiteHat.Common.Static.Constant;
using WhiteHat.Common.Static.Enum;
using WhiteHat.Data.EntityMap;
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.MediatR.MediatRService.UserService
{
    public  record GetUserByIdQuery(Guid UserId) : IRequest<ServiceResponse<UserInfoModel>>; 
    public record GetUserByIdQueryHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper ,IMediator Mediator):
        IRequestHandler<GetUserByIdQuery, ServiceResponse<UserInfoModel>>
    {
        public async Task<ServiceResponse<UserInfoModel>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<UserInfoModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbUserInfo = await dbContext.UserInfoes
                    .Where(u => !u.IsDeleted && u.UserId == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);
                if(dbUserInfo != null)
                {
                    response.Result = Mapper.Map<UserInfoModel>(dbUserInfo);
                    response.Type = ServiceResponseTypes.SUCCESS;
                }
                else
                {
                    errors.Add(UserConstant.User_NotFound);
                    response.Type = ServiceResponseTypes.NOTFOUND;
                    response.ErrorCode = (HttpStatusCode.NotFound).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
