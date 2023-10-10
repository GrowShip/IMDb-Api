using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static MediaApi.Structure.Country;

namespace MediaApi.Structure
{
    public class Language
    {
        public static Country MakeCountry()
        {
            Country country = new Country();
            country.MyList.Add(new MyObject { Id = 0, Country = " ", Symbol = "" });
            country.MyList.Add(new MyObject { Id = 1, Country = "Argentina", Symbol = "ar" });
            country.MyList.Add(new MyObject { Id = 2, Country = "Austria", Symbol = "at" });
            country.MyList.Add(new MyObject { Id = 3, Country = "Brazil", Symbol = "br" });
            country.MyList.Add(new MyObject { Id = 4, Country = "Chile", Symbol = "cl" });
            country.MyList.Add(new MyObject { Id = 5, Country = "China", Symbol = "cn" });
            country.MyList.Add(new MyObject { Id = 6, Country = "Colombia", Symbol = "co" });
            country.MyList.Add(new MyObject { Id = 7, Country = "Costa Rica", Symbol = "cr" });
            country.MyList.Add(new MyObject { Id = 8, Country = "Egypt", Symbol = "eg" });
            country.MyList.Add(new MyObject { Id = 9, Country = "France", Symbol = "fr" });
            country.MyList.Add(new MyObject { Id = 10, Country = "Germany", Symbol = "de" });
            country.MyList.Add(new MyObject { Id = 11, Country = "Hong Kong", Symbol = "hk" });
            country.MyList.Add(new MyObject { Id = 12, Country = "India", Symbol = "in" });
            country.MyList.Add(new MyObject { Id = 13, Country = "Italy", Symbol = "it" });
            country.MyList.Add(new MyObject { Id = 14, Country = "Kazakhstan", Symbol = "kz" });
            country.MyList.Add(new MyObject { Id = 15,Country = "Korea", Symbol = "xko" });
            country.MyList.Add(new MyObject { Id = 16, Country = "Mexico", Symbol = "mx" });
            country.MyList.Add(new MyObject { Id = 17, Country = "Panama", Symbol = "pa" });
            country.MyList.Add(new MyObject { Id = 18, Country = "Russia", Symbol = "ru" });
            country.MyList.Add(new MyObject { Id = 19, Country = "Saudi Arabia", Symbol = "sa" });
            country.MyList.Add(new MyObject { Id = 20, Country = "South Korea", Symbol = "kr" });
            country.MyList.Add(new MyObject { Id = 21, Country = "Spain", Symbol = "es" });
            country.MyList.Add(new MyObject { Id = 22, Country = "Taiwan", Symbol = "tw" });
            country.MyList.Add(new MyObject { Id = 23, Country = "Turkey", Symbol = "tr" });
            country.MyList.Add(new MyObject { Id = 24, Country = "United Kingdom", Symbol = "gb" });
            country.MyList.Add(new MyObject { Id = 25, Country = "United States", Symbol = "us" });
           
            return country;
        }

        public static Dictionary<string, string> countryCodeDictionary = new Dictionary<string, string>()
        {
            { " ", "" },
            { "ar", "Argentina" },
            { "at"   , "Austria"},
            { "br","Brazil" },
            { "cl","Chile" },
            { "cn"   , "China"},
            { "co", "Colombia" },
            {"cr", "Costa Rica" },
            { "eg"  , "Egypt"},
            { "fr"   , "France"},
            { "de"   , "Germany"},
            { "hk"   , "Hong Kong"},
            { "in"   , "India"},
            { "it"   , "Italy"},
            { "kz"   , "Kazakhstan"},
            { "xko"  , "Korea"},
            {"mx", "Mexico" },
            {"pa","Panama" },
            { "ru"   , "Russia"},
            { "sa"  , "Saudi Arabia"},
            { "kr"   , "South Korea"},
            { "es"   , "Spain"},
            { "tw"   , "Taiwan"},
            { "tr"   , "Turkey"},
            { "gb"   , "United Kingdom"},
            { "us"   , "United States"}
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
