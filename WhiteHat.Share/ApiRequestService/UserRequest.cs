using WhiteHat.Common.ServiceResponse;
using WhiteHat.Share.ApiHttpService;
using WhiteHat.Share.Constant;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Share.ApiRequestService
{

    public interface IUserRequest
    {
        Task<ServiceResponse<List<UserInfoModel>>> GetUsers();
        Task<ServiceResponse<UserInfoModel>> PostUser(UserInfoModel UserInfoModel);
        Task<ServiceResponse<UserInfoModel>> UpdateUser(UserInfoModel UserInfoModel);
        Task<ServiceResponse<UserInfoModel>> GetUserById(Guid id);
        Task<ServiceResponse<bool>> DeleteUser(Guid id);
        //Task<ServiceResponse<List<UserInfoModel>>> GetUserByFilter(UserInfoFilterModel filterModel);

    }

    public class UserRequest : IUserRequest
    {
        private readonly IHttpService _httpService;

        public UserRequest(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<ServiceResponse<List<UserInfoModel>>> GetUsers() =>
        _httpService.Get<ServiceResponse<List<UserInfoModel>>>(ApiUri.UserUri);

        public async Task<ServiceResponse<UserInfoModel>?> GetUserById(Guid id) =>
         await _httpService.Get<ServiceResponse<UserInfoModel>>(
             ApiUri.UserUri + ApiUri.Slash + $"{id}");

        public async Task<ServiceResponse<UserInfoModel>> PostUser(UserInfoModel UserInfoModel) =>
            await _httpService.Post<UserInfoModel, ServiceResponse<UserInfoModel>>(
                ApiUri.AdminUserUri, UserInfoModel);


        public async Task<ServiceResponse<bool>> DeleteUser(Guid id) =>
            await _httpService.Delete<ServiceResponse<bool>>(
                ApiUri.UserUri + ApiUri.Slash + $"{id}");

        public Task<ServiceResponse<UserInfoModel>> UpdateUser(UserInfoModel UserInfoModel) =>
            _httpService.Put<UserInfoModel, ServiceResponse<UserInfoModel>>(
                ApiUri.UserUri + ApiUri.Slash + $"{UserInfoModel.UserId}", UserInfoModel);

        //public async Task<ServiceResponse<List<UserInfoModel>>> GetUserByFilter(UserInfoFilterModel filterModel) =>
        //    await _httpService.Post<UserInfoFilterModel, ServiceResponse<List<UserInfoModel>>>(
        //        ApiUri.UserUri + ApiUri.Filter, filterModel);

    }
}
