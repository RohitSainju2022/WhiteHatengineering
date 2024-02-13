using WhiteHat.Common.Static.Enum;
using WhiteHat.Share.ApiRequestService;
using WhiteHat.Share.Constant;
using WhiteHat.Share.Services.CoreServices;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Share.Services.User
{
    public interface IUserService
    {
        Task<List<UserInfoModel>> GetUsers();
        Task<UserInfoModel> GetUserById(Guid id);
        Task<(bool, string)> PostUser(UserInfoModel model);
        Task<(bool, string)> UpdateUser(UserInfoModel model);
        Task<bool> DeleteUser(Guid id);
        //Task<List<UserInfoModel>> GetUsersByFilter(UserInfoFilterModel filterModel);
        Task<UserInfoModel> GetCurrentUser();
    }
    public class UserService : IUserService
    {
        private readonly IUserRequest _userRequest;
        private readonly ISessionState<AuthenticationModel> _session;
        private static readonly string LocalStorageKey = StorageKeyConstant.StorageKeyName;

        public UserService(IUserRequest Userrequest, ISessionState<AuthenticationModel> session)
        {
            _userRequest = Userrequest;
            _session = session;
        }
        public async Task<List<UserInfoModel>> GetUsers()
        {
            var response = await _userRequest.GetUsers();
            if (response.Type == ServiceResponseTypes.SUCCESS)
            {
                return response.Result;
            }
            return new List<UserInfoModel>();
        }

        public async Task<UserInfoModel> GetUserById(Guid id)
        {
            var response = await _userRequest.GetUserById(id);
            if (response.Type == ServiceResponseTypes.SUCCESS)
            {
                return response.Result;
            }
            return new UserInfoModel();
        }

        public async Task<(bool, string)> PostUser(UserInfoModel model)
        {
            var response = await _userRequest.PostUser(model);
            if (response.Result == null)
            {
                return (false, response.ErrorMessage);
            }
            return (true, response.Result.UserId.ToString());
        }

        public async Task<(bool, string)> UpdateUser(UserInfoModel model)
        {
            var response = await _userRequest.UpdateUser(model);
            if (response.Result == null)
            {
                return (false, response.ErrorMessage);
            }
            return (true, response.Result.UserId.ToString());
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var response = await _userRequest.DeleteUser(id);
            return response.Result;
        }

        //public async Task<List<UserInfoModel>> GetUsersByFilter(UserInfoFilterModel filterModel)
        //{
        //    var response = await _userRequest.GetUserByFilter(filterModel);
        //    if (response.Type == ServiceResponseTypes.SUCCESS)
        //    {
        //        return response.Result;
        //    }
        //    return new List<UserInfoModel>();
        //}

        public async Task<UserInfoModel> GetCurrentUser()
        {
            var session = await _session.Get(LocalStorageKey);
            if (session == null)
            {
                return new();
            }

            var result = await _userRequest.GetUserById(session.User.UserId);

            if (result != null && result.Type == ServiceResponseTypes.SUCCESS)
            {
                return result.Result;
            }
            return new();

        }
    }
}
