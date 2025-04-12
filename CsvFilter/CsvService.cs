using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvFilter;

internal static class CsvService
{
    public static void FilterCsvFiles()
    {
        // Gets a dictionary with fileName as Key and filePath as Value.
        Dictionary<string, string> files = GetFiles();

        if (files.Count <= 0)
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

    private static List<GuestParsed> ParseGuestList(IEnumerable<Guest> guests)
    {
        List<GuestParsed> guestsParsed = new();
        string ouiResterInforme = "Oui";

        HashSet<string> emails = [];

        foreach (Guest guest in guests)
        {
            if (emails.Contains(guest.Email) == false && guest.ResterInforme == ouiResterInforme)
            {
                emails.Add(guest.Email);
                guestsParsed.Add(new GuestParsed
                {
                    GuestFirstName = guest.GuestFirstName,
                    GuestLastName = guest.GuestLastName,
                    Email = guest.Email,
                    TelephoneMobile = guest.TelephoneMobile,
                    ResterInforme = guest.ResterInforme
                });
            }
        }

        return guestsParsed;
    }

    private static void WriteCsv(List<GuestParsed> guestParseds, string fileName)
    {
        string projectPath = Path.GetFullPath("../../..");

        // Suppose there is a path to *project*/files/
        string filesFolderPath = Path.Combine(projectPath, "files");

        string processedFolderPath = Path.Combine(filesFolderPath, "processed");

        Directory.CreateDirectory(processedFolderPath);

        // Adding a prefix for the names of the parsed files.
        fileName = string.Concat("Parsed - ", fileName);
        string csvPath = Path.Combine(processedFolderPath, fileName);

        using var writer = new StreamWriter(csvPath);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        };
        using var csv = new CsvWriter(writer, config);

        csv.WriteRecords(guestParseds);
    }

    // Returns a Dictionary with fileName as Key and filePath as Value.
    private static Dictionary<string, string> GetFiles()
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