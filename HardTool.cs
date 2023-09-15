using IMDbApiLib.Models;
using IMDbApiLib;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MediaApi.Structure;

namespace MediaApi
{
    internal class HardTool
    {
        public static FilmData GetSavedJson(string system)
        {
            return GetSavedJsonMethod(system);
        }

        private static FilmData GetSavedJsonMethod(string system)
        {
            FilmData savedJson;
            string json = "";
            if (File.Exists($"JSON\\{system}\\file.json"))
            {
                json = File.ReadAllText($"JSON\\{system}\\file.json");
            }
            else
            {
                string path = @"JSON";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                path = @"JSON\" + system;
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                var a = MessageBox.Show("Создать архивный файл?","Архивного файла нет",MessageBoxButtons.YesNo);
                
                if (a == DialogResult.Yes)
                {
                    FilmData filmData = new FilmData();
                    filmData.Results = new List<JsonData>();

                    SaveJson(JsonConvert.SerializeObject(filmData), system);
                }
            }

            if (!string.IsNullOrEmpty(json))
            {
                savedJson = JsonConvert.DeserializeObject<FilmData>(json);
            }
            else
            {
                savedJson = new FilmData();
                savedJson.Results = new List<JsonData>();
            }
            return savedJson;
        }

        public static void SaveJson(string json, string system)
        {
            try
            {
                File.WriteAllText($"JSON\\{system}\\file.json", json);
                Console.WriteLine("JSON file saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON file: {ex.Message}");
            }
        }

        public static void SaveJson(FilmData data, string system)
        {
            try
            {
                SaveJson(JsonConvert.SerializeObject(data), system);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error saving JSON file: {ex.Message}");
            }
        }

        public static void MakeAnArchive(string system)
        {
            var oldNameFullPath = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\{system}\\file.json";
            var newNameFullPath = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\{system}\\{DateTime.Now.ToShortDateString()}.json";
            RemoveAnArchive(newNameFullPath);
            File.Move(oldNameFullPath,newNameFullPath);
        }

        public static void RemoveAnArchive(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void AddToActiveJson(List <FilmData> title)
        {

        }
    }

    internal class ExcelPlace
    {
        public static void SaveReleasesToExcel(FilmData savedJson, bool onlyNewCheckBox, List<string> newAddedJson)
        {
            SaveReleasesToExceMethod(savedJson, onlyNewCheckBox, newAddedJson);
        }

        private static void SaveReleasesToExceMethod(FilmData savedJson, bool onlyNewCheckBox, List<string> newAddedJson)
        {
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("All");

                // Set column headers
                worksheet.Cells[1, 1].Value = "Type";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 3].Value = "Airline Release";
                worksheet.Cells[1, 4].Value = "Genre";
                worksheet.Cells[1, 5].Value = "Studio";
                worksheet.Cells[1, 6].Value = "Year";
                worksheet.Cells[1, 7].Value = "Rating IMDB";
                worksheet.Cells[1, 8].Value = "Award";
                worksheet.Cells[1, 9].Value = "Gross World";
                //worksheet.Cells[1, 10].Value = "Release Date";
                worksheet.Cells[1, 11].Value = "Release Country";
                worksheet.Cells[1, 12].Value = "Snopsys";
                worksheet.Cells[1, 13].Value = "Id";
                worksheet.Cells[1, 14].Value = "Runtime";
                worksheet.Cells[1, 15].Value = "Description";
                worksheet.Cells[1, 16].Value = "Directors";
                worksheet.Cells[1, 17].Value = "Stars";
                worksheet.Cells[1, 18].Value = "US";
                worksheet.Cells[1, 19].Value = "DE";
                worksheet.Cells[1, 20].Value = "IT";
                worksheet.Cells[1, 21].Value = "ES";
                worksheet.Cells[1, 22].Value = "GB";
                worksheet.Cells[1, 23].Value = "FR";
                worksheet.Cells[1, 24].Value = "CN";
                worksheet.Cells[1, 25].Value = "RU";

                // Parse the JSON response and populate the Excel worksheet
                // Replace this code with your own JSON parsing logic
                // Here, we assume the JSON response is an array of objects,
                // each representing a release with 'name', 'year', 'synopsis', and 'other' properties

                

                //AdvancedSearchData releases = JsonConvert.DeserializeObject<AdvancedSearchData>(responseJson);
                int row = 2;
                if (onlyNewCheckBox)
                {
                    try
                    {
                        foreach (var release in newAddedJson)
                        {
                            worksheet.Cells[row, 1].Value = savedJson.Results.Find(f => f.Id == release).Type;
                            worksheet.Cells[row, 2].Value = savedJson.Results.Find(f => f.Id == release).Title;
                            worksheet.Cells[row, 4].Value = savedJson.Results.Find(f => f.Id == release).Genres;
                            //worksheet.Cells[row, 5].Value = savedJson.Results.Find(f => f.Id == release).LocationSearch;
                            worksheet.Cells[row, 6].Value = savedJson.Results.Find(f => f.Id == release).Year;
                            worksheet.Cells[row, 7].Value = savedJson.Results.Find(f => f.Id == release).IMDbRating;
                            worksheet.Cells[row, 8].Value = savedJson.Results.Find(f => f.Id == release).Awards;
                            worksheet.Cells[row, 9].Value = savedJson.Results.Find(f => f.Id == release).GrossWorld;
                            //worksheet.Cells[row, 10].Value = savedJson.Results.Find(f => f.Id == release).ReleaseDate;
                            worksheet.Cells[row, 11].Value = savedJson.Results.Find(f => f.Id == release).Countries;
                            worksheet.Cells[row, 12].Value = savedJson.Results.Find(f => f.Id == release).Plot;
                            worksheet.Cells[row, 13].Value = savedJson.Results.Find(f => f.Id == release).Id;
                            worksheet.Cells[row, 14].Value = savedJson.Results.Find(f => f.Id == release).RuntimeStr;
                            worksheet.Cells[row, 15].Value = savedJson.Results.Find(f => f.Id == release).Description;
                            worksheet.Cells[row, 16].Value = savedJson.Results.Find(f => f.Id == release).Directors;
                            worksheet.Cells[row, 17].Value = savedJson.Results.Find(f => f.Id == release).Stars;
                            if (savedJson.Results.Find(f => f.Id == release).All is not null)
                            {
                                worksheet.Cells[row, 18].Value = savedJson.Results.Find(f => f.Id == release).All.US.releaseDate;
                                worksheet.Cells[row, 19].Value = savedJson.Results.Find(f => f.Id == release).All.DE.releaseDate;
                                worksheet.Cells[row, 20].Value = savedJson.Results.Find(f => f.Id == release).All.IT.releaseDate;
                                worksheet.Cells[row, 21].Value = savedJson.Results.Find(f => f.Id == release).All.ES.releaseDate;
                                worksheet.Cells[row, 22].Value = savedJson.Results.Find(f => f.Id == release).All.GB.releaseDate;
                                worksheet.Cells[row, 23].Value = savedJson.Results.Find(f => f.Id == release).All.FR.releaseDate;
                                worksheet.Cells[row, 24].Value = savedJson.Results.Find(f => f.Id == release).All.CN.releaseDate;
                                worksheet.Cells[row, 25].Value = savedJson.Results.Find(f => f.Id == release).All.RU.releaseDate;
                            }
                            row++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Я не смог добавить: \r" + ex.Message);
                        throw;
                    }
                }
                else
                {
                    foreach (var release in savedJson.Results)
                    {

                        worksheet.Cells[row, 1].Value = release.Type;
                        worksheet.Cells[row, 2].Value = release.Title;
                        worksheet.Cells[row, 4].Value = release.Genres;
                        //worksheet.Cells[row, 5].Value = release.LocationSearch;
                        worksheet.Cells[row, 6].Value = release.Year;
                        worksheet.Cells[row, 7].Value = release.IMDbRating;
                        worksheet.Cells[row, 8].Value = release.Awards;
                        worksheet.Cells[row, 9].Value = release.GrossWorld;
                        //worksheet.Cells[row, 10].Value = release.ReleaseDate;
                        worksheet.Cells[row, 11].Value = release.Countries;
                        worksheet.Cells[row, 12].Value = release.Plot;
                        worksheet.Cells[row, 13].Value = release.Id;
                        worksheet.Cells[row, 14].Value = release.RuntimeStr;
                        worksheet.Cells[row, 15].Value = release.Description;
                        worksheet.Cells[row, 16].Value = release.Directors;
                        worksheet.Cells[row, 17].Value = release.Stars;
                        if (release.All is not null)
                        {
                            worksheet.Cells[row, 18].Value = release.All.US.releaseDate;
                            worksheet.Cells[row, 19].Value = release.All.DE.releaseDate;
                            worksheet.Cells[row, 20].Value = release.All.IT.releaseDate;
                            worksheet.Cells[row, 21].Value = release.All.ES.releaseDate;
                            worksheet.Cells[row, 22].Value = release.All.GB.releaseDate;
                            worksheet.Cells[row, 23].Value = release.All.FR.releaseDate;
                            worksheet.Cells[row, 24].Value = release.All.CN.releaseDate;
                            worksheet.Cells[row, 25].Value = release.All.RU.releaseDate;
                        }
                        row++;
                    }
                }

                //width              
                worksheet.Columns[1].Width = 7;     //Type;
                worksheet.Columns[2].Width = 35;    //Title;
                worksheet.Columns[3].Width = 17;    //Airline Release;
                worksheet.Columns[4].Width = 29;    //Genre;
                worksheet.Columns[5].Width = 22;     //Studia;
                worksheet.Columns[6].Width = 11;    //Year;
                worksheet.Columns[7].Width = 11;    //Rating IMDB;
                worksheet.Columns[8].Width = 16;     //Awards
                worksheet.Columns[9].Width = 16;     //Gross
                worksheet.Columns[10].Width = 3;    //
                worksheet.Columns[11].Width = 22;   //Release Сountry;
                worksheet.Columns[12].Width = 22;   //Snopsys;
                worksheet.Columns[13].Width = 10;   //Id;
                worksheet.Columns[14].Width = 8;    //Runtime;
                worksheet.Columns[15].Width = 10;   //Description;
                worksheet.Columns[16].Width = 16;   //Directors;
                worksheet.Columns[17].Width = 16;   //Stars;
                worksheet.Columns[18].Width = 17;   //US;
                worksheet.Columns[19].Width = 17;   //DE;
                worksheet.Columns[20].Width = 17;   //IT;
                worksheet.Columns[21].Width = 17;   //ES;
                worksheet.Columns[22].Width = 17;   //GB;
                worksheet.Columns[23].Width = 17;   //FR;
                worksheet.Columns[24].Width = 17;   //CN;
                worksheet.Columns[25].Width = 17;   //RU;

                worksheet.Row(1).Style.Font.Bold = true;

                // Save the Excel file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save Releases";
                saveFileDialog.ShowDialog();

                if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    FileInfo file = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(file);
                    MessageBox.Show("Releases saved successfully!");
                }
            }
        }

    }

    internal class Requests
    {
        /// <summary>
        /// Get запрос для api шек
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="apiKey"></param>
        /// <returns>В формате строки json</returns>
        public static string GET(Uri uri,string apiKey)
        {
            var requ = WebRequest.Create(uri);

            requ.ContentType = "application/json";
            requ.Headers["X-API-KEY"] = apiKey;
            requ.Method = "GET";

            string result;

            using (var stream = new StreamReader(requ.GetResponse().GetResponseStream()))
            {
                result = stream.ReadToEnd();
            }

            return result;
        }
    }
}
