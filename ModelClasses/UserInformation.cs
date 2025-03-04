
namespace ModelClasses
{
    public class UserInformation : ModelBase
    {
        public string loginId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string imgName { get; set; }
        public string role { get; set; }
        public int id { get; set; }
        public string recordUserId { get; set; }
        public string recordDate { get; set; }
        public string lastRecordModifyUser { get; set; }
        public string lastModifyDate { get; set; }
        public string status { get; set; }
    }
}
