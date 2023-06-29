using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDbApi
{
    internal class HardTool
    {
        public static FilmData GetSavedJson()
        {
            return GetSavedJsonMethod();
        }

        private static FilmData GetSavedJsonMethod()
        {
            FilmData savedJson;
            string json = "";
            if (File.Exists(@"JSON\file.json"))
            {
                json = File.ReadAllText(@"JSON\file.json");
            }
            else
            {
                string path = @"JSON";
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                MessageBox.Show("Архивного файла нет");
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

        public static void SaveJson(string json)
        {
            try
            {
                File.WriteAllText(@"JSON\file.json", json);
                Console.WriteLine("JSON file saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving JSON file: {ex.Message}");
            }
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
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson));

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("All");

                // Set column headers
                worksheet.Cells[1, 1].Value = "Type";
                worksheet.Cells[1, 2].Value = "Title";
                worksheet.Cells[1, 4].Value = "Genre";
                worksheet.Cells[1, 5].Value = "Country";
                worksheet.Cells[1, 6].Value = "Year";
                worksheet.Cells[1, 7].Value = "Rating IMDB";
                worksheet.Cells[1, 10].Value = "Release Date";
                worksheet.Cells[1, 11].Value = "Release Country";
                worksheet.Cells[1, 12].Value = "Snopsys";
                worksheet.Cells[1, 13].Value = "Id";
                worksheet.Cells[1, 14].Value = "Runtime";
                worksheet.Cells[1, 15].Value = "Description";
                worksheet.Cells[1, 16].Value = "Directors";
                worksheet.Cells[1, 17].Value = "Stars";

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
                            worksheet.Cells[row, 5].Value = savedJson.Results.Find(f => f.Id == release).LocationSearch;
                            worksheet.Cells[row, 6].Value = savedJson.Results.Find(f => f.Id == release).Year;
                            worksheet.Cells[row, 7].Value = savedJson.Results.Find(f => f.Id == release).IMDbRating;
                            worksheet.Cells[row, 10].Value = savedJson.Results.Find(f => f.Id == release).ReleaseDate;
                            worksheet.Cells[row, 11].Value = savedJson.Results.Find(f => f.Id == release).Countries;
                            worksheet.Cells[row, 12].Value = savedJson.Results.Find(f => f.Id == release).Plot;
                            worksheet.Cells[row, 13].Value = savedJson.Results.Find(f => f.Id == release).Id;
                            worksheet.Cells[row, 14].Value = savedJson.Results.Find(f => f.Id == release).RuntimeStr;
                            worksheet.Cells[row, 15].Value = savedJson.Results.Find(f => f.Id == release).Description;
                            worksheet.Cells[row, 16].Value = savedJson.Results.Find(f => f.Id == release).Directors;
                            worksheet.Cells[row, 17].Value = savedJson.Results.Find(f => f.Id == release).Stars;
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
                        worksheet.Cells[row, 5].Value = release.LocationSearch;
                        worksheet.Cells[row, 6].Value = release.Year;
                        worksheet.Cells[row, 7].Value = release.IMDbRating;
                        worksheet.Cells[row, 10].Value = release.ReleaseDate;
                        worksheet.Cells[row, 11].Value = release.Countries;
                        worksheet.Cells[row, 12].Value = release.Plot;
                        worksheet.Cells[row, 13].Value = release.Id;
                        worksheet.Cells[row, 14].Value = release.RuntimeStr;
                        worksheet.Cells[row, 15].Value = release.Description;
                        worksheet.Cells[row, 17].Value = release.Stars;
                        row++;
                    }
                }


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
}
