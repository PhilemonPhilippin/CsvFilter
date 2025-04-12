using CsvHelper.Configuration.Attributes;

namespace CsvFilter;

internal class Guest
{
    [Name("Order number")]
    public string OrderNumber { get; set; }
    [Name("Order date")]
    public string OrderDate { get; set; }
    [Name("Guest first name")]
    public string GuestFirstName { get; set; }
    [Name("Guest last name")]
    public string GuestLastName { get; set; }
    [Name("Email")]
    public string Email { get; set; }
    [Name("Ticket type")]
    public string TicketType { get; set; }
    [Name("Ticket number")]
    public string TicketNumber { get; set; }
    [Name("Ticket price")]
    public string TicketPrice { get; set; }
    [Name("Benefit")]
    public string? Benefit { get; set; }
    [Name("Coupon")]
    public string? Coupon { get; set; }
    [Name("Tax")]
    public string Tax { get; set; }
    [Name("Total ticket price")]
    public string TotalTicketPrice { get; set; }
    [Name("Wix service fee")]
    public string WixServiceFee { get; set; }
    [Name("Ticket revenue")]
    public string TicketRevenue { get; set; }
    [Name("Payment status")]
    public string PaymentStatus { get; set; }
    [Name("Checked in")]
    public string CheckedIn { get; set; }
    [Name("Seat Information")]
    public string? SeatInformation { get; set; }
    [Name("Téléphone mobile")]
    public string TelephoneMobile { get; set; }
    [Name("Rester informé(e) des prochains spectacles par email")]
    public string ResterInforme { get; set; }

}