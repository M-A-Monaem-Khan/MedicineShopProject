using ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaiVaiPharmacyModelClass.Stock
{
    public class StockBuyetails:ModelBase
    {
        public int stockId { get; set; }
        public string medicineName { get; set; }
        public int quentity { get; set; }
        public decimal actualPrice { get; set; }
        public decimal dicountPrice { get; set; }
        public decimal buyPrice { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
