using WhiteHat.Common.Static.Enum;
using WhiteHat.Share.ApiRequestService;
using WhiteHat.Share.Constant;
using WhiteHat.Share.Services.CoreServices;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Share.Services.ConstructionService
{
    public interface IConstructionService
    {
        Task<List<ConstructionModel>> GetConstructions();
        Task<ConstructionModel> GetConstructionById(int id);
        Task<(bool, string)> PostConstruction(ConstructionModel model);
        Task<(bool, string)> UpdateConstruction(ConstructionModel model);
        Task<bool> DeleteConstruction(int id);
    }
    public class ConstructionService : IConstructionService
    {
        private readonly IConstructionRequest _constructionRequest;
        private readonly ISessionState<AuthenticationModel> _session;
        private static readonly string LocalStorageKey = StorageKeyConstant.StorageKeyName;

        public ConstructionService(IConstructionRequest Constructionrequest, ISessionState<AuthenticationModel> session)
        {
            _constructionRequest = Constructionrequest;
            _session = session;
        }
        public async Task<List<ConstructionModel>> GetConstructions()
        {
            var response = await _constructionRequest.GetConstructions();
            if (response.Type == ServiceResponseTypes.SUCCESS)
            {
                return response.Result;
            }
            return new List<ConstructionModel>();
        }

        public async Task<ConstructionModel> GetConstructionById(int id)
        {
            var response = await _constructionRequest.GetConstructionById(id);
            if (response.Type == ServiceResponseTypes.SUCCESS)
            {
                return response.Result;
            }
            return new ConstructionModel();
        }

        public async Task<(bool, string)> PostConstruction(ConstructionModel model)
        {
            var response = await _constructionRequest.PostConstruction(model);
            if (response.Result == null)
            {
                return (false, response.ErrorMessage);
            }
            return (true, response.Result.ConstructionId.ToString());
        }

        public async Task<(bool, string)> UpdateConstruction(ConstructionModel model)
        {
            var response = await _constructionRequest.UpdateConstruction(model);
            if (response.Result == null)
            {
                return (false, response.ErrorMessage);
            }
            return (true, response.Result.ConstructionId.ToString());
        }

        public async Task<bool> DeleteConstruction(int id)
        {
            var response = await _constructionRequest.DeleteConstruction(id);
            return response.Result;
        }
    }
}
