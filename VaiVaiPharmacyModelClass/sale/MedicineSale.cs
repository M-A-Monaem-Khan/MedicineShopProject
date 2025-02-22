using ModelClasses;

namespace VaiVaiPharmacyModelClass.sale
{
    public class MedicineSale:ModelBase
    {
        public string customerName {  get; set; }
        public string customerPhone { get; set; }
        public string saleDate { get; set; }
        public string salesManName { get; set; }
        public int totalSale {  get; set; }
        public decimal totalPrice { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
