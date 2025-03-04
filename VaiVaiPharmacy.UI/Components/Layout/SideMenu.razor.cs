using ModelClasses;
using UXComponents;
using VaiVaiPharmacy.UI.Models;
using VaiVaiPharmacy.UI.Service;


namespace VaiVaiPharmacy.UI.Components.Layout
{
    public partial class SideMenu
    {
        public string inputPageNum = "";
        private string userImg = "img/avatars/user.png";
        private string currentDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
        private string BrandName = "";
        private string CopyWrite = "℗Monaem";
        private bool isShowOwnerName = false;
        private List<FunctionModel> functions = new List<FunctionModel>();
        private Dictionary<string, List<FunctionModel>> ModuleWiseItem = new Dictionary<string, List<FunctionModel>>();
        private List<Dropdown> UserList = new List<Dropdown>();
        private UserInformation _UserInformation = new UserInformation();
        private bool isUserModalShow = false;
        protected override async Task OnInitializedAsync()
        {
            BrandName = _config["BrandShortName"];

            UserInformationService userInfoService = new UserInformationService(_env);
            _UserInformation = await userInfoService.getUserInfoByUserId(await _sessionData.getSession());
            if (!string.IsNullOrEmpty(_UserInformation.imgName))
            {
                userImg = "userInfo/Img/" + _UserInformation.imgName;
            }
            if (_UserInformation.role == "Admin")
            {
                AdminFunctionList adminFunList = new AdminFunctionList(_env);
                var adminList = await adminFunList.GetFunctionList();
                ModuleWiseItem["Admin"] = adminList;
            }

            await LoadRouteList();
        }

        private async Task LoadRouteList()
        {
            FunctionList funList = new FunctionList(_env);
            //functions = await funList.GetFunctionList();
            //ModuleWiseItem["Medicine List"] = functions;
            functions = await funList.GetMedicineFunctionList();
            ModuleWiseItem["Medicine"] = functions;
            functions = await funList.GetstockFunctionList();
            ModuleWiseItem["Stock"] = functions;
            functions = await funList.GetMedSellFunctionList();
            ModuleWiseItem["Sale"] = functions;
        }
        private void RouteChange(string inputPageNum)
        {
            navigationManager.NavigateTo(inputPageNum);
        }
        private void UserModal()
        {
            isUserModalShow = true;
        }

        private void NavigetToReports()
        {

        }

        private void NavigetToApproval()
        {

        }

        private async Task Logout()
        {
            _sessionData.deleteSession();
            navigationManager.NavigateTo("/Login", forceLoad: true);
        }
    }
}
