using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvFilter;

internal static class CsvService
{
    public static void FilterCsvFiles()
    {
        // Gets a dictionary with fileName as Key and filePath as Value.
        Dictionary<string, string> files = GetFilePaths();

        if (files.Any() == false)
            return;

        foreach (KeyValuePair<string, string> kvp in files)
        {
            List<GuestParsed> guestsParsed = ParseGuestList(ReadCsv(kvp.Value));
            WriteCsv(guestsParsed, kvp.Key);
        }
    }
    private static IEnumerable<Guest> ReadCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<Guest>().ToList();
    }

    private static List<GuestParsed> ParseGuestList(IEnumerable<Guest> records)
    {
        List<GuestParsed> guestsParsed = new();
        string ouiResterInforme = "Oui";

        HashSet<string> emails = [];

        foreach (Guest record in records)
        {
            if (emails.Contains(record.Email) == false && record.ResterInforme == ouiResterInforme)
            {
                emails.Add(record.Email);
                guestsParsed.Add(new GuestParsed
                {
                    GuestFirstName = record.GuestFirstName,
                    GuestLastName = record.GuestLastName,
                    Email = record.Email,
                    TelephoneMobile = record.TelephoneMobile,
                    ResterInforme = record.ResterInforme
                });
            }
        }
        return guestsParsed;
    }

    private static void WriteCsv(List<GuestParsed> guestParseds, string fileName)
    {
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/processed
        string folderPath = Path.Combine(projectPath, "files", "processed");
        
        fileName = string.Concat("Parsed - ", fileName);
        string csvPath = Path.Combine(folderPath, fileName);

        using var writer = new StreamWriter(csvPath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };

        using var csv = new CsvWriter(writer, config);
        csv.WriteRecords(guestParseds);
    }

    // Returns a Dictionary with fileName as Key and filePath as Value.
    private static Dictionary<string, string> GetFilePaths()
    {
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/toProcess/*myFile*.csv
        string folderPath = Path.Combine(projectPath, "files", "toProcess");
        string[] filePaths = Directory.GetFiles(folderPath);
        List<string> fileNames = [];
        foreach (string filePath in filePaths)
        {
            int indexOfLastSlash = filePath.LastIndexOf("\\");
            string fileName = filePath.Substring(indexOfLastSlash + 1);
            fileNames.Add(fileName);
        }

        Dictionary<string, string> files = new();
        for (int i = 0; i < filePaths.Length; i++)
        {
            files.Add(fileNames[i], filePaths[i]);
        }

        return files;
    }
}