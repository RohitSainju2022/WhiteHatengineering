using WhiteHat.Common.ServiceResponse;
using WhiteHat.Share.ApiHttpService;
using WhiteHat.Share.Constant;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Share.ApiRequestService
{
    public interface IConstructionRequest
    {
        Task<ServiceResponse<List<ConstructionModel>>> GetConstructions();
        Task<ServiceResponse<ConstructionModel>> PostConstruction(ConstructionModel ConstructionModel);
        Task<ServiceResponse<ConstructionModel>> UpdateConstruction(ConstructionModel ConstructionModel);
        Task<ServiceResponse<ConstructionModel>> GetConstructionById(int id);
        Task<ServiceResponse<bool>> DeleteConstruction(int id);

    }

    public class ConstructionRequest : IConstructionRequest
    {
        private readonly IHttpService _httpService;

        public ConstructionRequest(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<ServiceResponse<List<ConstructionModel>>> GetConstructions() =>
        _httpService.Get<ServiceResponse<List<ConstructionModel>>>(ApiUri.UserUri);

        public async Task<ServiceResponse<ConstructionModel>?> GetConstructionById(int id) =>
         await _httpService.Get<ServiceResponse<ConstructionModel>>(
             ApiUri.UserUri + ApiUri.Slash + $"{id}");

        public async Task<ServiceResponse<ConstructionModel>> PostConstruction(ConstructionModel ConstructionModel) =>
            await _httpService.Post<ConstructionModel, ServiceResponse<ConstructionModel>>(
                ApiUri.AdminUserUri, ConstructionModel);


        public async Task<ServiceResponse<bool>> DeleteConstruction(int id) =>
            await _httpService.Delete<ServiceResponse<bool>>(
                ApiUri.UserUri + ApiUri.Slash + $"{id}");

        public Task<ServiceResponse<ConstructionModel>> UpdateConstruction(ConstructionModel ConstructionModel) =>
            _httpService.Put<ConstructionModel, ServiceResponse<ConstructionModel>>(
                ApiUri.UserUri + ApiUri.Slash + $"{ConstructionModel.ConstructionId}", ConstructionModel);

    }
}
