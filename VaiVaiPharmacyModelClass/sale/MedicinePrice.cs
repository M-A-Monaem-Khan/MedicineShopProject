using ModelClasses;

namespace VaiVaiPharmacyModelClass.sale
{
    public class MedicinePrice : ModelBase
    {
        public int medId { get; set; }
        public string medicineName { get; set; }
        public decimal price { get; set; }
        public decimal avgSellPrice { get; set; }
        public decimal avgBuyPrice { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
