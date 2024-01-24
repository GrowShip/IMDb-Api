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
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualBasic;

namespace MediaApi
{
    internal class HardTool
    {
        public static FilmData GetSavedJson(string system)
        {
            string jsonPath = Path.Combine("JSON", system, "file.json");

            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                return !string.IsNullOrEmpty(json)
                    ? JsonConvert.DeserializeObject<FilmData>(json)
                    : new FilmData { Results = new List<JsonData>() };
            }

            CreateDirectoriesIfNotExist("JSON", system);

            DialogResult result = MessageBox.Show("Создать архивный файл?", "Архивного файла нет", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                FilmData filmData = new FilmData { Results = new List<JsonData>() };
                SaveJson(JsonConvert.SerializeObject(filmData), system);
                return filmData;
            }

            return new FilmData { Results = new List<JsonData>() };
        }

        private static void CreateDirectoriesIfNotExist(params string[] directories)
        {
            string path = @"JSON";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            foreach (var directory in directories)
            {
                path = Path.Combine("JSON", directory);
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
            }
        }

        public static void SaveJson(string json, string system)
        {
            File.WriteAllText(Path.Combine("JSON", system, "file.json"), json);
            Console.WriteLine("JSON file saved successfully!");
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

            string inserting = "";
            using (var filenameForm = new Forms.InputBoxForm())
            {
                if (filenameForm.ShowDialog() == DialogResult.OK)
                {
                    if (filenameForm.filename.Length > 0)
                        inserting = filenameForm.filename;
                }
                else inserting = DateTime.Now.ToShortDateString();
            }

            var newNameFullPath = AppDomain.CurrentDomain.BaseDirectory + $"JSON\\{system}\\{inserting}.json";
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
            // Save JSON to file
            HardTool.SaveJson(JsonConvert.SerializeObject(savedJson), "imdb");

            // Create a list of languages
            var langList = new List<string> { "US", "DE", "IT", "ES", "GB", "FR", "CN", "RU" };

            // Set up Excel package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("All");

                // Set column headers
                SetExcelColumnHeaders(worksheet, langList);

                // Populate Excel worksheet
                PopulateExcelWorksheet(worksheet, savedJson.Results, newAddedJson, langList, onlyNewCheckBox);

                // Set column widths
                SetExcelColumnWidths(worksheet);

                // Save the Excel file
                SaveExcelFile(package);
            }
        }

        private static void SetExcelColumnHeaders(ExcelWorksheet worksheet, List<string> langList)
        {
            string[] columnHeaders =
            {
                "Type", "Title origin", "Title RUS", "Genre", "Studio", "Year", "Rating IMDB", "Award", "Gross World",
                "Airline Release", "Release Country", "Synopsis", "Id", "Runtime", "Description", "Directors", "Stars"
            };

            for (int i = 0; i < columnHeaders.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = columnHeaders[i];
            }

            for (int i = 0; i < langList.Count; i++)
            {
                worksheet.Cells[1, columnHeaders.Length + i + 1].Value = langList[i];
            }
        }

        private static void PopulateExcelWorksheet(ExcelWorksheet worksheet, List<JsonData> releases, List<string> newAddedJson, List<string> langList, bool onlyNewCheckBox)
        {
            int row = 2;

            foreach (var release in onlyNewCheckBox ? releases.Where(r => newAddedJson.Contains(r.Id)) : releases)
            {
                worksheet.Cells[row, 1].Value = release.Type;
                worksheet.Cells[row, 2].Value = release.Title;
                worksheet.Cells[row, 3].Value = release.TitleRus;
                worksheet.Cells[row, 4].Value = release.Genres;
                //worksheet.Cells[row, 5].Value = release.Studio;
                worksheet.Cells[row, 6].Value = release.Year;
                worksheet.Cells[row, 7].Value = release.IMDbRating;
                worksheet.Cells[row, 8].Value = release.Awards;
                worksheet.Cells[row, 9].Value = release.GrossWorld;
                //worksheet.Cells[row, 10].Value = release.AirlineRelease;
                worksheet.Cells[row, 11].Value = release.Countries;
                worksheet.Cells[row, 12].Value = release.Plot;
                worksheet.Cells[row, 13].Value = release.Id;
                worksheet.Cells[row, 14].Value = release.RuntimeStr;
                worksheet.Cells[row, 15].Value = release.Description;
                worksheet.Cells[row, 16].Value = release.Directors;
                worksheet.Cells[row, 17].Value = release.Stars;

                if (release.CounrtyReleaseAll != null)
                {
                    for (int i = 0; i < langList.Count; i++)
                    {
                        var CR = new CounrtyRelease();
                        if (release.CounrtyReleaseAll.TryGetValue(langList[i], out CR))
                            worksheet.Cells[row, 18 + i].Value = CR.releaseDate;
                    }
                }

                row++;
            }
        }

        private static void SetExcelColumnWidths(ExcelWorksheet worksheet)
        {
            int[] columnWidths = { 7, 35, 35, 29, 22, 11, 11, 16, 16, 17, 22, 22, 10, 8, 10, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17 };
            worksheet.Row(1).Style.Font.Bold = true;

            for (int i = 0; i < columnWidths.Length; i++)
            {
                worksheet.Columns[i + 1].Width = columnWidths[i];
            }
        }

        private static void SaveExcelFile(ExcelPackage package)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save Releases"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);
                package.SaveAs(file);
                MessageBox.Show("Releases saved successfully!");
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
