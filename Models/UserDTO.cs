using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReforgedApi.Models
{
    public class UserDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string countryCode { get; set; }
        public string favoriteFactionImg { get; set; }
        public List<Score> scores { get; set; }
    }
}