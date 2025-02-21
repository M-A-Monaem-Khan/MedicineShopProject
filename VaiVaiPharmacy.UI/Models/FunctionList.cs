using System.Text.Json;

namespace VaiVaiPharmacy.UI.Models
{
    public class FunctionList
    {
        private List<FunctionModel> function = new List<FunctionModel>();
        private string _jsonPath = "";
        public FunctionList(IWebHostEnvironment env)
        {
            _jsonPath = Path.Combine(env.WebRootPath, "FunctionData/FunctionList.json");
        }

        public async Task<List<FunctionModel>> GetFunctionList()
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
    }
}
