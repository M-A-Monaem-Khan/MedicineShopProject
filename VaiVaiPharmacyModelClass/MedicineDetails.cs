using ModelClasses;

namespace VaiVaiPharmacyModelClass
{
    public class MedicineDetails : ModelBase
    {
        public string medicineName { get; set; }
        public string companyName { get; set; }
        public string genericName { get; set; }
        public string type {  get; set; }
        public string stength { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
