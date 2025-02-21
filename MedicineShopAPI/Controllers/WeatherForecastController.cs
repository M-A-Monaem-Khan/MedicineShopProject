using Licence;
using Microsoft.AspNetCore.Mvc;
using ModelClasses;
using System.Text;
using System.Text.Json;

namespace MedicineShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _jsonFilePath;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _githubGistId = "fe848b481b0a395e40df350a63ac5d21";
        private readonly string _githubApiUrl = "";
        private readonly string _personalAccessToken = "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";

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment env, HttpClient httpClient)
        {
            _logger = logger;
            _env = env;
            _jsonFilePath = Path.Combine(_env.WebRootPath, "DataFiles/dummyTest.json");
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Monaem Khan");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _personalAccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _githubApiUrl = $"https://api.github.com/gists/{_githubGistId}";
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        public class UserData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserData newUser)
        {
            string errorMsg = string.Empty;
            if (newUser == null)
                return BadRequest("Invalid user data.");

            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            var userList = JsonSerializer.Deserialize<List<UserData>>(jsonData) ?? new List<UserData>();

            // Assign new ID (Auto-increment)
            newUser.Id = userList.Any() ? userList.Max(u => u.Id) + 1 : 1;
            userList.Add(newUser);

            await System.IO.File.WriteAllTextAsync(_jsonFilePath, JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true }));


            ResponceView<UserData> view = new ResponceView<UserData>
            {
                data = newUser,
                ErrorMsg = errorMsg
            };
            return Ok(view);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserData>>> GetAll()
        {
            try
            {
                List<UserData> medicines = await getAllTestData();
                return Ok(medicines);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private async Task<List<UserData>> getAllTestData()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://gist.githubusercontent.com/M-A-Monaem-Khan/fe848b481b0a395e40df350a63ac5d21/raw/test.json?t={DateTime.UtcNow.Ticks}");

            if (!response.IsSuccessStatusCode)
                return new List<UserData>();

            string json = await response.Content.ReadAsStringAsync();
            List<UserData> medicines = new List<UserData>();
            if (!string.IsNullOrEmpty(json))
                medicines = JsonSerializer.Deserialize<List<UserData>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return medicines;
        }


        [HttpPost("AddTest")]
        public async Task<IActionResult> AddMedicine([FromBody] UserData medicine)
        {
            if (medicine == null)
            {
                return BadRequest("Invalid medicine data.");
            }

            var medicineList = await getAllTestData();
            medicineList.Add(medicine);

            string updatedJson = JsonSerializer.Serialize(medicineList, new JsonSerializerOptions { WriteIndented = true });

            var payload = new
            {
                files = new Dictionary<string, object>
        {
            { "test.json", new { content = updatedJson } }
        }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Patch, _githubApiUrl)
            {
                Content = content
            };

            var updateResponse = await _httpClient.SendAsync(request);

            if (updateResponse.IsSuccessStatusCode)
            {
                return Ok("Medicine added successfully!");
            }

            string errorResponse = await updateResponse.Content.ReadAsStringAsync();
            return StatusCode((int)updateResponse.StatusCode, $"Failed to update Gist: {errorResponse}");
        }

        [HttpGet("GetLicenceCheck")]
        public async Task<IActionResult> LicenceChecking()
        {
            LicenceCheck licenceCheck = new LicenceCheck();

            return Ok(licenceCheck.isLicenceExpire());
        }
        [HttpGet("GetLicenceRemainingDate")]
        public async Task<IActionResult> LicenceRemainingDate()
        {
            LicenceCheck licenceCheck = new LicenceCheck();

            return Ok(licenceCheck.remainLicenceDate());
        }
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                // Optionally save the image to a file, database, or whatever else you need.
                // For now, we will just return the byte array.

                return Ok(Convert.ToBase64String( imageBytes));  // Return image bytes to the client
            }
        }


    }
}
