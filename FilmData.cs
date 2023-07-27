using IMDbApiLib.Models;
using MediaApi.IMDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaApi
{
    public class FilmData
    {
        public List<JsonData> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class JsonData
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string RuntimeStr { get; set; }
        public string Genres { get; set; }
        public string ContentRating { get; set; }
        public string IMDbRating { get; set; }
        public string Plot { get; set; }
        public string Stars { get; set; }
        
        public string LocationSearch { get; set; }

        public string Type { get; set; }
        public string Year { get; set; }
        public string ReleaseDate { get; set; }
        public string Awards { get; set; }
        public string Directors { get; set; }
        public string Companies { get; set; }
        public string Countries { get; set; }
        public string Languages { get; set; }
    }

    public class Converter
    {
        public static FilmData AdvToData(AdvancedSearchData AdvancedSearchDataJson, string language) 
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            foreach (var item in AdvancedSearchDataJson.Results)
            {
                var el = new JsonData();
                el.Id = item.Id;
                el.Title = item.Title;
                el.Genres = item.Genres;
                el.Description = item.Description;
                el.Image = item.Image;
                el.ContentRating = item.ContentRating;
                el.IMDbRating = item.IMDbRating;
                el.Stars = item.Stars;
                el.Plot = item.Plot;
                el.LocationSearch = language;
                result.Results.Add(el); 
            }
            return result;
        }

        public static FilmData TitleToData(TitleData item)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            var el = new JsonData();
            el.Id = item.Id;
            el.Title = item.Title;
            el.Type = item.Type;
            el.Year = item.Year;
            el.Image = item.Image;
            el.ReleaseDate = item.ReleaseDate;
            el.RuntimeStr = item.RuntimeMins;
            el.Plot = item.Plot;
            el.Awards = item.Awards;
            el.Directors = item.Directors;
            el.Stars = item.Stars;
            el.Genres = item.Genres;
            el.Companies = item.Companies;
            el.Countries = item.Countries;
            el.Languages = item.Languages;
            el.ContentRating = item.ContentRating;
            el.IMDbRating = item.IMDbRating;
            result.Results.Add(el);

            return result;
        }

        public static FilmData NewMovieToData(NewMovieData item)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            foreach (var elem in item.Items)
            {
                var el = new JsonData();
                el.Id =             elem.Id;
                el.Title =          elem.Title;
                el.Year =           elem.Year;
                el.Image =          elem.Image;
                el.ReleaseDate =    elem.ReleaseState;
                el.RuntimeStr =     elem.RuntimeMins;
                el.Plot =           elem.Plot;
                el.Directors =      elem.Directors;
                el.Stars =          elem.Stars;
                el.Genres =         elem.Genres;
                el.ContentRating =  elem.ContentRating;
                el.IMDbRating =     elem.IMDbRating;

                result.Results.Add(el);
            }
            return result;
        }

        public static FilmData CalendarToData(CalendarData data)
        {
            var result = new FilmData();
            result.Results = new List<JsonData>();

            for (int i = 0; i < data.props.pageProps.groups.Count; i++)
            {
                for (int j = 0; j < data.props.pageProps.groups[i].entries.Count; j++)
                {
                    var item = data.props.pageProps.groups[i].entries[j];
                    JsonData el = new JsonData();

                    el.Id = item.id;
                    el.Title = item.titleText;
                    el.Image = item.imageModel != null ?  item.imageModel.url : null ;
                    //el.Image = item.imageModel.url;
                    //el.Description =
                    //el.RuntimeStr =
                    el.Genres = string.Join(", ", item.genres);
                    //el.ContentRating =
                    //el.IMDbRating =
                    //el.Plot =
                    //el.Stars =
                    //el.LocationSearch =
                    el.Type = item.titleType.text;
                    el.Year = item.releaseYear.year.ToString();
                    el.ReleaseDate = item.releaseDate.Substring(item.releaseDate.IndexOf(",")+1,12);
                    //el.Awards =
                    //el.Directors =
                    //el.Companies =
                    //el.Countries =
                    //el.Languages = 
                    result.Results.Add(el);
                }
            }

            return result;
        }
    }
}
