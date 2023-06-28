using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static IMDbApi.Country;

namespace IMDbApi
{
    public class Language
    {
        public static Country MakeCountry()
        {
            Country country = new Country();
            
            country.MyList.Add(new MyObject { Id = 1, Country =  "Austria", Symbol = "at"  });
            country.MyList.Add(new MyObject { Id = 2, Country =  "China", Symbol = "cn"  });
            country.MyList.Add(new MyObject { Id = 3, Country =  "East German", Symbol = "ddde" });
            country.MyList.Add(new MyObject { Id = 4, Country =  "France", Symbol = "fr"  });
            country.MyList.Add(new MyObject { Id = 5, Country =  "Germany", Symbol = "de"  });
            country.MyList.Add(new MyObject { Id = 6, Country =  "Hong Kong", Symbol = "hk"  });
            country.MyList.Add(new MyObject { Id = 7, Country =  "India", Symbol = "in"  });
            country.MyList.Add(new MyObject { Id = 8, Country =  "Kazakhstan", Symbol = "kz"  });
            country.MyList.Add(new MyObject { Id = 9, Country =  "Korea", Symbol = "xko" });
            country.MyList.Add(new MyObject { Id = 10, Country = "Russia", Symbol = "ru"  });
            country.MyList.Add(new MyObject { Id = 11, Country = "South Korea", Symbol = "kr"  });
            country.MyList.Add(new MyObject { Id = 12, Country = "Spain", Symbol = "es"  });
            country.MyList.Add(new MyObject { Id = 13, Country = "Taiwan", Symbol = "tw"  });
            country.MyList.Add(new MyObject { Id = 14, Country = "Turkey", Symbol = "tr"  });
            country.MyList.Add(new MyObject { Id = 15, Country = "United Kingdom", Symbol = "gb"  });
            country.MyList.Add(new MyObject { Id = 16, Country = "United States", Symbol = "us"  });
            country.MyList.Add(new MyObject { Id = 17, Country = "West Germany", Symbol = "xwg" });
            return country;

        }

        public static Dictionary<string,string> countryCodeDictionary = new Dictionary<string, string>()
        {
            { "at"   , "Austria"},
            { "cn"   , "China"},
            { "ddde" , "East German"},
            { "fr"   , "France"},
            { "de"   , "Germany"},
            { "hk"   , "Hong Kong"},
            { "in"   , "India"},
            { "kz"   , "Kazakhstan"},
            { "xko"  , "Korea"},
            { "ru"   , "Russia"},
            { "kr"   , "South Korea"},
            { "es"   , "Spain"},
            { "tw"   , "Taiwan"},
            { "tr"   , "Turkey"},
            { "gb"   , "United Kingdom"},
            { "us"   , "United States"},
            { "xwg"  , "West Germany"}
        };

    }
    public class Country
    {
        public List<MyObject> MyList { get; set; }
        public class MyObject
        {
            public int Id { get; set; }
            public string Country { get; set; }
            public string Symbol { get; set; }
        }
        public Country()
        {
            MyList = new List<MyObject>();
        }

    }
}
