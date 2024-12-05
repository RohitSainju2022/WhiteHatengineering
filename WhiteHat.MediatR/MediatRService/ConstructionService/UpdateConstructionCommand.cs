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
    public record UpdateConstructionCommand(int ConstructionId, ConstructionModel ConstructionModel) : IRequest<ServiceResponse<ConstructionModel>>;

    public record UpdateConstructionCommandHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator)
        : IRequestHandler<UpdateConstructionCommand, ServiceResponse<ConstructionModel>>
    {
        public async Task<ServiceResponse<ConstructionModel>> Handle(UpdateConstructionCommand request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<ConstructionModel> response = new();
            List<string> errors = new();
            using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var dbConstruction = await dbContext.Constructions
                    .Where(c => c.ConstructionId == request.ConstructionId && !c.IsDeleted)
                    .FirstOrDefaultAsync(cancellationToken);

                if (dbConstruction != null)
                {
                    Mapper.Map(request.ConstructionModel, dbConstruction);
                    dbContext.Entry(dbConstruction).State = EntityState.Modified;
                    var result = await dbContext.SaveChangesAsync(cancellationToken);
                    if (result != 0)
                    {
                        response.Result = request.ConstructionModel;
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
