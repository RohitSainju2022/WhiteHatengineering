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
    public record GetConstructionsQuery : IRequest<ServiceResponse<List<ConstructionModel>>>;

    public record GetConstructionsQueryHandler(IDbContextFactory<ApplicationDbContext> DbContextfactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<GetConstructionsQuery, ServiceResponse<List<ConstructionModel>>>
    {
        public async Task<ServiceResponse<List<ConstructionModel>>> Handle(GetConstructionsQuery request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<List<ConstructionModel>> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextfactory.CreateDbContextAsync(cancellationToken))
            {
                var dbConstructions = await dbContext.Constructions
                    .Where(u => !u.IsDeleted)
                    .ToListAsync(cancellationToken);
                if (dbConstructions.Count > 0)
                {
                    response.Result = Mapper.Map<List<ConstructionModel>>(dbConstructions);
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
