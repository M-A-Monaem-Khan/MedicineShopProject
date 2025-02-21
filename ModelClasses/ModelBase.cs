using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClasses
{
    public class ModelBase
    {
        public int id { get; set; } = 0;
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
