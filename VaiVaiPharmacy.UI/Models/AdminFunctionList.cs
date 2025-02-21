using System.Text.Json;

namespace VaiVaiPharmacy.UI.Models
{
    public class AdminFunctionList
    {
        private List<FunctionModel> function = new List<FunctionModel>();
        private string _jsonPath = "";
        public AdminFunctionList(IWebHostEnvironment env)
        {
            _jsonPath = Path.Combine(env.WebRootPath, "FunctionData/adminFunction.json");
        }

        public async Task<List<FunctionModel>> GetFunctionList()
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
    }
}
