using ModelClasses;
using System.Text.Json;

namespace VaiVaiPharmacy.UI.Service
{
    public class UserInformationService
    {
        private string _jsonUserInfoPath = "";
        private List<UserInformation> _userList = new List<UserInformation>();
        public UserInformationService(IWebHostEnvironment env)
        {
            _jsonUserInfoPath = Path.Combine(env.WebRootPath, "userInfo/userInfo.json");
        }
        private async Task GetUserList()
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonUserInfoPath);
            _userList = JsonSerializer.Deserialize<List<UserInformation>>(jsonData) ?? new List<UserInformation>();
        }
        private async Task SaveUserList(List<UserInformation> userInfo)
        {
            await System.IO.File.WriteAllTextAsync(_jsonUserInfoPath, JsonSerializer.Serialize(_userList, new JsonSerializerOptions { WriteIndented = true }));
        }

        public async Task<bool> isLoginValid(string userId, string password)
        {
            bool isValid = false;
            await GetUserList();
            foreach (var user in _userList)
            {
                if (userId == user.loginId && user.password == password && user.status != "DEL")
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        public async Task<bool> isDuplicateUserId(string userId)
        {
            bool isDuplicate = false;
            await GetUserList();
            foreach (var user in _userList)
            {
                if (userId == user.loginId)
                {
                    isDuplicate = true;
                    break;
                }
            }
            return isDuplicate;
        }

        public async Task<UserInformation> saveOrUpdateOrDeleteUser(UserInformation _userInfo, string userId, bool delete = false)
        {
            await GetUserList();
            if (_userInfo.id == 0)
            {
                var id = 1;
                if (_userList.Count != 0)
                {
                    id = _userList.Max(x => x.id) + 1;
                }
                _userInfo.id = id;
                _userInfo.recordUserId = userId;
                _userInfo.recordDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                _userInfo.status = "ADD";
                _userList.Add(_userInfo);
            }
            else
            {
                var newList = new List<UserInformation>();
                foreach (var user in _userList)
                {
                    if (_userInfo.id == user.id)
                    {
                        _userInfo.lastRecordModifyUser = userId;
                        _userInfo.lastModifyDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                        if (delete)
                            _userInfo.status = "DEL";
                        else
                            _userInfo.status = "EDT";
                        newList.Add(_userInfo);
                    }
                    else
                    {
                        newList.Add(user);
                    }
                }
                _userList = newList;
            }

            await SaveUserList(_userList);

            return _userInfo;
        }

        public async Task<UserInformation> getUserInfoByUserId(string userid)
        {
            UserInformation obj = new UserInformation();
            await GetUserList();
            foreach (var item in _userList)
            {
                if (item.loginId == userid)
                {
                    obj = item;
                    break;
                }
            }
            return obj;

        }
        public async Task<List<UserInformation>> getAllUser()
        {
            await GetUserList();
            return _userList;

        }
    }
}
