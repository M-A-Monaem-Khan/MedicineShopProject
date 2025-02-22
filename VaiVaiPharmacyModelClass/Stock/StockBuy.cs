using ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaiVaiPharmacyModelClass.Stock
{
    public class StockBuy:ModelBase
    {
        public string buyerName { get; set; }
        public string buyerPhone { get; set; }
        public string buyDate { get; set; }
        public int totalBuy { get; set; }
        public decimal totalPrice { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
