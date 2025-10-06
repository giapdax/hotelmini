using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InvoiceDisplay
{
    public int InvoiceID { get; set; }
    public int BookingID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerIDNumber { get; set; }
    public DateTime? IssuedAt { get; set; }
    public decimal RoomCharge { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal Surcharge { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}
