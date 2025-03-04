using ModelClasses;

namespace VaiVaiPharmacyModelClass.sale
{
    public class MedicineSaleDetails : ModelBase
    {
        public int sellId { get; set; }
        public int medicineid { get; set; }
        public string medicineName { get; set; }
        public int quentity { get; set; }
        public decimal actualPrice { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal discountPrice { get; set; }
        public decimal sellPrice { get; set; }
        public decimal avgBuyPrice { get; set; }
        public decimal profitGain { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
