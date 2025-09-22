using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HOTEL_MINI.BLL
{
    public class PdfExportService
    {
        private readonly BaseFont _vietnameseFont;

        public PdfExportService()
        {
            try
            {
                // Thư mục Fonts trong project (nằm cạnh file .exe)
                string fontsDirectory = Path.Combine(Application.StartupPath, "Fonts");

                // Nếu chưa có thư mục thì tạo
                if (!Directory.Exists(fontsDirectory))
                {
                    Directory.CreateDirectory(fontsDirectory);
                }

                // Đường dẫn đến file font
                string fontPath = Path.Combine(fontsDirectory, "tahoma.ttf");

                if (!File.Exists(fontPath))
                {
                    MessageBox.Show("Không tìm thấy file font tahoma.ttf trong thư mục Fonts.\nVui lòng copy font vào đó.",
                        "Thiếu Font", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // fallback: Arial mặc định của Windows
                    string systemFont = @"C:\Windows\Fonts\arial.ttf";
                    if (File.Exists(systemFont))
                        _vietnameseFont = BaseFont.CreateFont(systemFont, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    else
                        _vietnameseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                }
                else
                {
                    // Load font từ thư mục project
                    _vietnameseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load font: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _vietnameseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            }

        }

        private Font GetVietnameseFont(float size, int style = Font.NORMAL)
        {
            return new Font(_vietnameseFont, size, style);
        }

        public void ExportInvoiceToPdf(Invoice invoice, Booking booking, string roomNumber,
                                     List<UsedServiceDto> usedServices, string filePath)
        {
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.Open();

                // Title
                Paragraph title = new Paragraph("HÓA ĐƠN THANH TOÁN", GetVietnameseFont(18, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                // Invoice Info
                PdfPTable infoTable = new PdfPTable(2) { WidthPercentage = 100 };
                infoTable.SetWidths(new float[] { 30, 70 });

                AddInfoRow(infoTable, "Mã hóa đơn:", invoice.InvoiceID.ToString());
                AddInfoRow(infoTable, "Phòng:", roomNumber);
                AddInfoRow(infoTable, "Ngày check-in:", booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm"));
                AddInfoRow(infoTable, "Ngày check-out:", booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm"));
                AddInfoRow(infoTable, "Ngày xuất hóa đơn:", invoice.IssuedAt.ToString("dd/MM/yyyy HH:mm"));

                document.Add(infoTable);
                document.Add(new Paragraph(" "));

                // Services
                if (usedServices != null && usedServices.Count > 0)
                {
                    Paragraph servicesTitle = new Paragraph("DỊCH VỤ SỬ DỤNG", GetVietnameseFont(12, Font.BOLD))
                    {
                        SpacingAfter = 10f
                    };
                    document.Add(servicesTitle);

                    PdfPTable servicesTable = new PdfPTable(3) { WidthPercentage = 100 };
                    servicesTable.SetWidths(new float[] { 50, 20, 30 });

                    AddTableHeader(servicesTable, "Tên dịch vụ");
                    AddTableHeader(servicesTable, "Số lượng");
                    AddTableHeader(servicesTable, "Thành tiền");

                    foreach (var service in usedServices)
                    {
                        AddTableRow(servicesTable, service.ServiceName);
                        AddTableRow(servicesTable, service.Quantity.ToString());
                        AddTableRow(servicesTable, service.Total.ToString("N0") + " đ");
                    }

                    document.Add(servicesTable);
                    document.Add(new Paragraph(" "));
                }

                // Summary
                PdfPTable summaryTable = new PdfPTable(2)
                {
                    WidthPercentage = 50,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };

                AddSummaryRow(summaryTable, "Tiền phòng:", invoice.RoomCharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Tiền dịch vụ:", invoice.ServiceCharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Phụ phí:", invoice.Surcharge.ToString("N0") + " đ");
                AddSummaryRow(summaryTable, "Giảm giá:", invoice.Discount.ToString("N0") + " đ");

                // Total
                PdfPCell totalLabel = new PdfPCell(new Phrase("TỔNG CỘNG:", GetVietnameseFont(12, Font.BOLD)))
                {
                    Border = PdfPCell.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };

                PdfPCell totalValue = new PdfPCell(new Phrase(invoice.TotalAmount.ToString("N0") + " đ", GetVietnameseFont(12, Font.BOLD)))
                {
                    Border = PdfPCell.NO_BORDER
                };

                summaryTable.AddCell(totalLabel);
                summaryTable.AddCell(totalValue);

                document.Add(summaryTable);
                document.Add(new Paragraph(" "));

                // Footer
                    Paragraph footer = new Paragraph("Cảm ơn quý khách đã sử dụng dịch vụ!", GetVietnameseFont(10, Font.ITALIC))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f
                };
                document.Add(footer);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xuất PDF: " + ex.Message);
            }
            finally
            {
                document.Close();
            }
        }

        private void AddInfoRow(PdfPTable table, string label, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(label, GetVietnameseFont(10, Font.BOLD)))
            {
                Border = PdfPCell.NO_BORDER,
                Padding = 5
            });

            table.AddCell(new PdfPCell(new Phrase(value ?? "N/A", GetVietnameseFont(10)))
            {
                Border = PdfPCell.NO_BORDER,
                Padding = 5
            });
        }

        private void AddTableHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, GetVietnameseFont(10, Font.BOLD)))
            {
                BackgroundColor = new BaseColor(200, 200, 200),
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            };
            table.AddCell(cell);
        }

        private void AddTableRow(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, GetVietnameseFont(10)))
            {
                Padding = 5
            };
            table.AddCell(cell);
        }

        private void AddSummaryRow(PdfPTable table, string label, string value)
        {
            table.AddCell(new PdfPCell(new Phrase(label, GetVietnameseFont(10)))
            {
                Border = PdfPCell.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT
            });

            table.AddCell(new PdfPCell(new Phrase(value, GetVietnameseFont(10)))
            {
                Border = PdfPCell.NO_BORDER
            });
        }
    }
}
