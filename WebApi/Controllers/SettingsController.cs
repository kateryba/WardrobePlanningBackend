using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("settings")]

    public class SettingsController : Controller
    {

        private static readonly Dictionaries Settings = new Dictionaries();

        public class Dictionaries
        {
            public int[] Sizes => new[]
            {
                50, 56, 62, 68, 74, 80, 86, 92, 98, 104, 5, 6, 7 , 8, 10, 12, 14, 16, 0
            };

            public string[] Owners => new[]
            {
            "Ann", "John", "Liz", "Unknown"
            };

            public string[] Types => new[]
            {
            "Top", "Bottom", "Overall", "Shoes", "Accessories", "Unknown"
            };

            public string[] Colors => new[]
            {
            "red", "orange", "yellow", "chartreuse green", "green", "spring green", "cyan", "azure", "blue", "violet", "magenta", "rose", "white", "black", "Unknown"
            };

            public string[] Seasons => new[]
            {
            "summer", "winter", "autumn-spring", "Unknown"
            };
            public string[] Styles => new[]
            {
            "boys", "girls", "uni", "Unknown"
            };

            public string[] FitTypes => new[]
            {
            "husky" , "slim", "regular", "Unknown"
            };

        }

        [HttpGet]
        public Dictionaries Get()
        {
            return Settings;
        }
    }
}
//        // get: api/<controller>
//        [httpget]
//        public ienumerable<string> get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // get api/<controller>/5
//        [httpget("{id}")]
//        public string get(int id)
//        {
//            return "value";
//        }

//        // post api/<controller>
//        [httppost]
//        public void post([frombody]string value)
//        {
//        }

//        // put api/<controller>/5
//        [httpput("{id}")]
//        public void put(int id, [frombody]string value)
//        {
//        }

//        // delete api/<controller>/5
//        [httpdelete("{id}")]
//        public void delete(int id)
//        {
//        }
//    }
//}
