using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Common.UI.Models
{
    public class sessionData
    {
        private readonly ProtectedSessionStorage _session;

        public sessionData(ProtectedSessionStorage sessionStorage)
        {
            _session = sessionStorage;
        }
        public async Task setSession(string userId)
        {
            await _session.SetAsync("userId", userId);
        }

        public async Task<string> getSession()
        {
            var result = await _session.GetAsync<string>("userId");
            if (result.Success)
            {
                return result.Value;
            }
            else
            {
                return "";
            }
        }

        public async Task deleteSession()
        {
            await _session.DeleteAsync("userId");
        }
    }
}
