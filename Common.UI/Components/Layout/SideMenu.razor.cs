using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Common.UI.Components.Layout
{
    public partial class SideMenu
    {
        public string inputPageNum = "";
        private string userImg = "";
        private string userName = "";
        private string userDescription = "";
        private string transDate = "";
        private string userHomeOffice = "";
        private string BrandName = "℗Monaem";
        private bool isShowOwnerName = false;



        private Dictionary<string,string> ModuleWiseItem = new Dictionary<string, string>();

        private void RouteChange(string inputPageNum)
        {

        }
        private void UserModal()
        {

        }

        private void NavigetToReports()
        {

        }

        private void NavigetToApproval()
        {

        }

        private void Logout()
        {

        }
    }
}
