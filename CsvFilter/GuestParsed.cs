using CsvHelper.Configuration.Attributes;

namespace CsvFilter;

internal class GuestParsed
{
    [Name("Guest first name")]
    public string GuestFirstName { get; set; }
    [Name("Guest last name")]
    public string GuestLastName { get; set; }
    [Name("Email")]
    public string Email { get; set; }
    [Name("Téléphone mobile")]
    public string TelephoneMobile { get; set; }
    [Name("Rester informé(e) des prochains spectacles par email")]
    public string ResterInforme { get; set; }
}