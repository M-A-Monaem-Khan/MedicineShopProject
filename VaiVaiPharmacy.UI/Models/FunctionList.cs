using System.Text.Json;

namespace VaiVaiPharmacy.UI.Models
{
    public class FunctionList
    {
        private List<FunctionModel> function = new List<FunctionModel>();
        private string _jsonPath = "";
        private IWebHostEnvironment _env;
        public FunctionList(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<FunctionModel>> GetFunctionList()
        {

            _jsonPath = Path.Combine(_env.WebRootPath, "FunctionData/FunctionList.json");
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
        
        public async Task<List<FunctionModel>> GetMedicineFunctionList()
        {
            _jsonPath = Path.Combine(_env.WebRootPath, "FunctionData/MedicineFunction.json");
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
        public async Task<List<FunctionModel>> GetstockFunctionList()
        {
            _jsonPath = Path.Combine(_env.WebRootPath, "FunctionData/stockFunction.json");
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
        public async Task<List<FunctionModel>> GetMedSellFunctionList()
        {
            _jsonPath = Path.Combine(_env.WebRootPath, "FunctionData/MedSellFunction.json");
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
            function = JsonSerializer.Deserialize<List<FunctionModel>>(jsonData) ?? new List<FunctionModel>();
            return function;
        }
    }
}
