using ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaiVaiPharmacyModelClass
{
    public class Company:ModelBase
    {
        public string companyName { get; set; }
        public string companyDetails { get; set; }
        int ModelBase.id { get; set; } = 0;
        string ModelBase.recordUserId { get; set; }
        string ModelBase.recordDate { get; set; }
        string ModelBase.lastRecordModifyUser { get; set; }
        string ModelBase.lastModifyDate { get; set; }
        string ModelBase.status { get; set; }
    }
}
