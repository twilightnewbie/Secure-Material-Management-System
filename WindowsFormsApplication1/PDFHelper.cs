using System;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WindowsFormsApplication1
{
    public static class PdfHelper
    {
        public static byte[] GenerateInvoicePdf(DataTable dt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Tiêu đề
                var title = new Paragraph("HÓA ĐƠN BÁN HÀNG\n\n",
                    new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                // Bảng PDF
                PdfPTable table = new PdfPTable(dt.Columns.Count);
                foreach (DataColumn col in dt.Columns)
                {
                    table.AddCell(new Phrase(col.ColumnName));
                }

                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                        table.AddCell(item.ToString());
                }

                doc.Add(table);
                doc.Close();

                return ms.ToArray();
            }
        }
    }
}
