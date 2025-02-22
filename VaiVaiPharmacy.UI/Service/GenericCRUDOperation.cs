using ModelClasses;
using System.Reflection;
using System.Text.Json;
using UXComponents;
using VaiVaiPharmacy.UI.Models;

namespace VaiVaiPharmacy.UI.Service
{
    public class GenericCRUDOperation
    {
        private readonly IWebHostEnvironment _env;
        private readonly sessionData _session; 
        private Dictionary<string, string> _primaryColumn = new Dictionary<string, string>()
        {
            {"Company","companyName" },
            {"MedicineDetails","medicineName" },
            {"stockAvailable","medicineName" },
            {"MedicinePrice","medicineName" }
        };
        public GenericCRUDOperation(IWebHostEnvironment env, sessionData storage)
        {
            _env = env;
            _session = storage;
        }

        private string dbFileName<T>()
        {
            string filename = "";
            filename = "Database/" + MethodeName<T>() + "Database.json";
            return filename;
        }
        private string MethodeName<T>()
        {
            string filename = "";
            MethodInfo[] methodInfos = typeof(T).GetMethods();
            filename = methodInfos[0].DeclaringType.Name;
            return filename;
        }
        private async Task<List<T>> getDataList<T>() where T : class, ModelBase
        {
            List<T> dataList = new List<T>();
            string _jsonPath = "";
            string pathName = dbFileName<T>();
            _jsonPath = Path.Combine(_env.WebRootPath, pathName);
            try
            {
                var jsonData = await System.IO.File.ReadAllTextAsync(_jsonPath);
                dataList = JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
            }
            catch { }
            return dataList;
        }
        private async Task saveDataList<T>(List<T> dataList) where T : class, ModelBase
        {
            string _jsonPath = "";
            string pathName = dbFileName<T>();
            _jsonPath = Path.Combine(_env.WebRootPath, pathName);
            try
            {
                await System.IO.File.WriteAllTextAsync(_jsonPath, JsonSerializer.Serialize(dataList, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch
            {
            }
        }
        public async Task<bool> isDuplicateValue<T>(string value,string columnName = null) where T : class, ModelBase
        {
            List<T> _dataList = new List<T>();
            _dataList = await getDataList<T>();

            string primaryColumn = columnName;

            if (string.IsNullOrEmpty(columnName))
            {
                string methodeName = MethodeName<T>();
                primaryColumn = _primaryColumn[methodeName];
            }

            var property = typeof(T).GetProperty(primaryColumn);

            return _dataList.Any(item => property.GetValue(item)?.Equals(value) == true);
        }
        public async Task<List<T>> getAll<T>() where T : class, ModelBase
        {
            List<T> _dataList = new List<T>();
            _dataList = await getDataList<T>();
            return _dataList;
        }
        public async Task<T> getInfoById<T>(string value,string columnName = null) where T : class, ModelBase
        {
            object obj = new object();
            List<T> _dataList = new List<T>();
            _dataList = await getDataList<T>();

            string primaryColumn = columnName;

            if (string.IsNullOrEmpty(columnName))
            {
                string methodeName = MethodeName<T>();
                primaryColumn = _primaryColumn[methodeName];
            }

            var property = typeof(T).GetProperty(primaryColumn);
            foreach (var item in _dataList)
            {
                var d = property.GetValue(item);
                if (d+"" == value)
                {
                    obj = item;
                    break;
                }
            }
            return (T)obj;

        }
        public async Task<T> saveOrUpdateOrDeleteUser<T>(T _info, bool delete = false) where T : class,ModelBase
        {
            List<T> _dataList = new List<T>();
            _dataList = await getDataList<T>();            

            if (_info.id == 0)
            {
                int id = 1;
                if (_dataList.Count != 0)
                {
                    id = _dataList.Max(x => x.id) + 1;
                }
                _info.id = id;
                _info.recordUserId = await _session.getSession();
                _info.recordDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                _info.status = "ADD";
                _dataList.Add(_info);
            }
            else
            {
                var newList = new List<T>();
                foreach (var user in _dataList)
                {
                    if (_info.id == user.id)
                    {
                        _info.lastRecordModifyUser = await _session.getSession();
                        _info.lastModifyDate = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                        if (delete)
                            _info.status = "DEL";
                        else
                            _info.status = "EDT";
                        newList.Add(_info);
                    }
                    else
                    {
                        newList.Add(user);
                    }
                }
                _dataList = newList;
            }

            await saveDataList<T>(_dataList);

            return _info;
        }
        public async Task<List<Dropdown>> getDropDown<T>(string columnName) where T : class,ModelBase
        {
            List<Dropdown> list = new List<Dropdown>();

            List<T> _dataList = new List<T>();
            _dataList = await getDataList<T>();

            var property = typeof(T).GetProperty(columnName);
            foreach (var item in _dataList.Where(x=>x.status != "DEL").ToList<T>())
            {
                var d = property.GetValue(item);
                list.Add(new Dropdown
                {
                    text = d+"",
                    value = d+""
                });
            }

            return list;
        }
    }
}

