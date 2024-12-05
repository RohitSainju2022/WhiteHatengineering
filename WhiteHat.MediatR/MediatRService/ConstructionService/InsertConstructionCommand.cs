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
using WhiteHat.Common.Static.Enum;
using WhiteHat.Data.WhiteHatDbContext;
using WhiteHat.Models;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.MediatR.MediatRService.ConstructionService
{
    public record InsertConstructionCommand(ConstructionModel ConstructionModel) : IRequest<ServiceResponse<ConstructionModel>>;
    public record InsertConstructionCommandHandler(IDbContextFactory<ApplicationDbContext> DbContextFactory,
        IMapper Mapper, IMediator Mediator) :
        IRequestHandler<InsertConstructionCommand, ServiceResponse<ConstructionModel>>
    {
        public async Task<ServiceResponse<ConstructionModel>> Handle(InsertConstructionCommand request, CancellationToken cancellationToken)
        {
            ServiceResponse<ConstructionModel> response = new();
            List<string> errors = new();
            if (request.ConstructionModel != null)
            {
                using (var dbContext = await DbContextFactory.CreateDbContextAsync(cancellationToken))
                {
                    var dbConstruction = ConstructionCustemMapper.ToEntity(new Construction(), request.ConstructionModel);
                    dbContext.Constructions.Add(dbConstruction);
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
