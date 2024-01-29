using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WhiteHat.Common.ServiceResponse;
using WhiteHat.Common.Static.Constant;
using WhiteHat.Common.Static.Enum;
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.MediatR.MediatRService.UserService
{
    public record GetUsersQuery :IRequest<ServiceResponse<List<UserInfoModel>>>;

    public record GetUsersHandler(IDbContextFactory<ApplicationDbContext> DbContextfactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetUsersQuery,ServiceResponse<List<UserInfoModel>>>
    {
        public async Task<ServiceResponse<List<UserInfoModel>>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<UserInfoModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextfactory.CreateDbContextAsync(cancellationToken)) 
            {
                var dbUserInfoes = await dbContext.UserInfoes
                    .Where(u => !u.IsDeleted)
                    .ToListAsync(cancellationToken);
                if(dbUserInfoes.Count > 0) 
                {
                    response.Result = Mapper.Map<List<UserInfoModel>>(dbUserInfoes);
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
