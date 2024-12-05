using AutoMapper;
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
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.MediatR.MediatRService.ConstructionService
{
    public record GetConstructionByIdQuery(int ConstructionId) : IRequest<ServiceResponse<ConstructionModel>>;
    public record GetConstructionByIdQueryHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetConstructionByIdQuery, ServiceResponse<ConstructionModel>>
    {
        public async Task<ServiceResponse<ConstructionModel>> Handle(GetConstructionByIdQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<ConstructionModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbConstruction = await dbContext.Constructions
                    .Where(u => !u.IsDeleted && u.ConstructionId == request.ConstructionId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (dbConstruction != null)
                {
                    response.Result = Mapper.Map<ConstructionModel>(dbConstruction);
                    response.Type = ServiceResponseTypes.SUCCESS;
                }
                else
                {
                    //errors.Add(UserConstant.User_NotFound);
                    response.Type = ServiceResponseTypes.NOTFOUND;
                    response.ErrorCode = (HttpStatusCode.NotFound).ToString();
                    response.Errors = errors;
                }
            }
            return response;
        }
    }
}
