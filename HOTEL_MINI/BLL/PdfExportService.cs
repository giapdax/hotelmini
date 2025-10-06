using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.BLL
{
    public class PdfExportService
    {
        private readonly BaseFont _vietnameseFont;
        private readonly string customerName;
        private readonly string customerID;

        public PdfExportService(string customerName, string customerCCCD)
        {
            this.customerName = customerName;
            this.customerID = customerCCCD;
            try
            {
                string fontsDirectory = Path.Combine(Application.StartupPath, "Fonts");
                if (!Directory.Exists(fontsDirectory)) Directory.CreateDirectory(fontsDirectory);
                string fontPath = Path.Combine(fontsDirectory, "tahoma.ttf");
                if (!File.Exists(fontPath))
                {
                    string systemFont = @"C:\Windows\Fonts\arial.ttf";
                    _vietnameseFont = File.Exists(systemFont)
                        ? BaseFont.CreateFont(systemFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                        : BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                }
                else
                {
                    _vietnameseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
            }
            catch
            {
                _vietnameseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }
        }

        private Font GetVietnameseFont(float size, int style = Font.NORMAL) => new Font(_vietnameseFont, size, style);

        public void ExportInvoiceToPdf(
            Invoice invoice,
            BookingRoom bookingLine,
            string roomNumber,
            List<UsedServiceDto> usedServices,
            string filePath)
        {
            string pricingType = "";
            decimal unitPrice = 0m;
            int quantity = 1;

            try
            {
                if (bookingLine != null && bookingLine.PricingID > 0)
                {
                    var pr = new RoomPricingRepository().GetPricingTypeById(bookingLine.PricingID);
                    if (pr != null)
                    {
                        pricingType = pr.PricingType ?? "";
                        unitPrice = pr.Price;
                    }
                }
            }
            catch { }

            var rooms = new List<(BookingRoom Room, string RoomNumber, string PricingType, decimal UnitPrice, int Quantity)>
            {
                (bookingLine, roomNumber ?? "", pricingType, unitPrice, quantity)
            };

            ExportInvoiceToPdf(invoice, rooms, usedServices, filePath);
        }

        public void ExportInvoiceToPdf(
            Invoice invoice,
            List<(BookingRoom Room, string RoomNumber, string PricingType, decimal UnitPrice, int Quantity)> rooms,
            List<UsedServiceDto> usedServices,
            string filePath)
        {
            var document = new Document(PageSize.A4, 50, 50, 50, 50);

            try
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                var title = new Paragraph("HÓA ĐƠN THANH TOÁN", GetVietnameseFont(18, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                var infoTable = new PdfPTable(2) { WidthPercentage = 100 };
                infoTable.SetWidths(new float[] { 30, 70 });
                AddInfoRow(infoTable, "Mã hóa đơn:", invoice.InvoiceID.ToString());
                AddInfoRow(infoTable, "Khách hàng:", customerName ?? "");
                AddInfoRow(infoTable, "Số CMND/CCCD:", customerID ?? "");
                AddInfoRow(infoTable, "Ngày xuất hóa đơn:", (invoice.IssuedAt ?? DateTime.Now).ToString("dd/MM/yyyy HH:mm"));
                document.Add(infoTable);
                document.Add(new Paragraph(" "));

                if (rooms != null && rooms.Count > 0)
                {
                    var roomTitle = new Paragraph("CHI TIẾT PHÒNG", GetVietnameseFont(12, Font.BOLD)) { SpacingAfter = 10f };
                    document.Add(roomTitle);

                    var roomTable = new PdfPTable(6) { WidthPercentage = 100 };
                    roomTable.SetWidths(new float[] { 16, 16, 18, 18, 16, 16 });
                    AddTableHeader(roomTable, "Phòng");
                    AddTableHeader(roomTable, "Loại giá");
                    AddTableHeader(roomTable, "Check-in");
                    AddTableHeader(roomTable, "Check-out");
                    AddTableHeader(roomTable, "Đơn giá");
                    AddTableHeader(roomTable, "Thành tiền");

                    foreach (var it in rooms)
                    {
                        var ci = it.Room?.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "—";
                        var co = it.Room?.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? "—";
                        var unit = it.UnitPrice > 0 ? it.UnitPrice.ToString("N0") + " đ" : "—";
                        var qty = it.Quantity <= 0 ? 1 : it.Quantity;
                        var lineTotal = qty * Math.Max(0, it.UnitPrice);
                        var totalStr = lineTotal > 0 ? lineTotal.ToString("N0") + " đ" : "—";

                        AddTableRow(roomTable, it.RoomNumber ?? "");
                        AddTableRow(roomTable, it.PricingType ?? "");
                        AddTableRow(roomTable, ci);
                        AddTableRow(roomTable, co);
                        AddTableRow(roomTable, unit);
                        AddTableRow(roomTable, totalStr);
                    }

                    document.Add(roomTable);
                    document.Add(new Paragraph(" "));
                }

                if (usedServices != null && usedServices.Count > 0)
                {
                    var serviceTitle = new Paragraph("DỊCH VỤ SỬ DỤNG", GetVietnameseFont(12, Font.BOLD)) { SpacingAfter = 10f };
                    document.Add(serviceTitle);

                    var serviceTable = new PdfPTable(4) { WidthPercentage = 100 };
                    serviceTable.SetWidths(new float[] { 40, 20, 20, 20 });

                    AddTableHeader(serviceTable, "Tên dịch vụ");
                    AddTableHeader(serviceTable, "Đơn giá");
                    AddTableHeader(serviceTable, "Số lượng");
                    AddTableHeader(serviceTable, "Thành tiền");

                    foreach (var s in usedServices)
                    {
                        AddTableRow(serviceTable, s.ServiceName ?? "");
                        AddTableRow(serviceTable, (s.Price).ToString("N0") + " đ");
                        AddTableRow(serviceTable, s.Quantity.ToString());
                        AddTableRow(serviceTable, (s.Price * s.Quantity).ToString("N0") + " đ");
                    }

                    document.Add(serviceTable);
                    document.Add(new Paragraph(" "));
                }

                var summaryTable = new PdfPTable(2)
                {
                    WidthPercentage = 50,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };
                AddSummaryRow(summaryTable, "Tiền phòng:", invoice.RoomCharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Tiền dịch vụ:", invoice.ServiceCharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Phụ phí:", invoice.Surcharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Giảm giá:", invoice.Discount.ToString("N0") + " đ");

                var totalLabel = new PdfPCell(new Phrase("TỔNG CỘNG:", GetVietnameseFont(12, Font.BOLD)))
                { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
                var totalValue = new PdfPCell(new Phrase(invoice.TotalAmount.ToString("N0") + " đ", GetVietnameseFont(12, Font.BOLD)))
                { Border = PdfPCell.NO_BORDER };
                summaryTable.AddCell(totalLabel);
                summaryTable.AddCell(totalValue);

                document.Add(summaryTable);
                document.Add(new Paragraph(" "));

                var footer = new Paragraph("Cảm ơn quý khách đã sử dụng dịch vụ!", GetVietnameseFont(10, Font.ITALIC))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f
                };
                document.Add(footer);
            }
            finally
            {
                document.Close();
            }

            try
            {
                var psi = new ProcessStartInfo(filePath) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch { }
        }

        private void AddInfoRow(PdfPTable table, string label, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(label, GetVietnameseFont(10, Font.BOLD))) { Border = PdfPCell.NO_BORDER, Padding = 5 });
            table.AddCell(new PdfPCell(new Phrase(value ?? "N/A", GetVietnameseFont(10))) { Border = PdfPCell.NO_BORDER, Padding = 5 });
        }

        private void AddTableHeader(PdfPTable table, string text)
        {
            var cell = new PdfPCell(new Phrase(text, GetVietnameseFont(10, Font.BOLD)))
            { BackgroundColor = new BaseColor(200, 200, 200), HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 };
            table.AddCell(cell);
        }

        private void AddTableRow(PdfPTable table, string text)
        {
            var cell = new PdfPCell(new Phrase(text, GetVietnameseFont(10))) { Padding = 5 };
            table.AddCell(cell);
        }

        private void AddSummaryRow(PdfPTable table, string label, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(label, GetVietnameseFont(10))) { Border = PdfPCell.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });
            table.AddCell(new PdfPCell(new Phrase(value, GetVietnameseFont(10))) { Border = PdfPCell.NO_BORDER });
        }
    }
}