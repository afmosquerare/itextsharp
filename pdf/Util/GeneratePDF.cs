using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using pdf.Models;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Principal;

namespace pdf.Util
{
    public class GeneratePDF
    {
        public GeneratePDF()
        {

        }


        public ResponseDocument GenerateInvestorDocument(FacturaModel m)
        {
            string filePath = @"Templates\";

            string fileNameExisting = @"Factura.pdf";

            string fileNewName = @"Factura_" + m.Cliente.Trim() + ".pdf";

            string fullNewPath = filePath + fileNewName;

            string fullExistingPath = filePath + fileNameExisting;


            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using (MemoryStream outputStream = new MemoryStream())
            {
                using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
                {

                    // encabezado
                    var pdfReader = new PdfReader(existingFileStream);
                    var stamper = new PdfStamper(pdfReader, outputStream);

                    AcroFields fields = stamper.AcroFields;
                    var plazo = ((m.FechaVence - m.FechaFactura).TotalDays).ToString() + " Dias";

                    BarcodeQRCode barcodeqrcode = new BarcodeQRCode("https://ekisa.com.co", 80, 80, null);
                    Image codeQrImage = barcodeqrcode.GetImage();
                    float x = fields.GetFieldPositions("QRCode")[0].position.Left;
                    float y = fields.GetFieldPositions("QRCode")[0].position.Bottom;
                    stamper.GetOverContent(1).AddImage(codeQrImage, codeQrImage.Width, 0, 0, codeQrImage.Height, x, y);


                    fields.SetField("Cliente", m.Cliente);
                    fields.SetField("IdCliente", m.IdCliente.ToString());
                    fields.SetField("DireccionCliente", m.DireccionCliente);
                    fields.SetField("CiudadCliente", m.CiudadCliente);
                    fields.SetField("TelefonoCliente", m.TelefonoCliente);
                    fields.SetField("NumeroFactura", m.NumeroFactura);
                    fields.SetField("FechaFactura", m.FechaFactura.ToString("yyyy-MM-dd"));
                    fields.SetField("FormaDePago", m.FormaDePago);
                    fields.SetField("FechaVence", m.FechaVence.ToString("yyyy-MM-dd"));
                    fields.SetField("Plazo", plazo);

                    // table productos
                    var outer = new PdfPTable(1);

                    Font fontTable = new Font(Font.FontFamily.HELVETICA, 8);
                    Font fontHeader = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD);

                    var table = new PdfPTable(21);
                    table.WidthPercentage = 100;

                    var data = new List<string[]>
                    {
                        new [] {"Producto", "Cant", "Vr Unitario", "Subtotal", "IVA", "Total Pro"},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro},
                        new [] {m.producto, m.cantidad, m.vlrUnitario, m.subtotal, m.iva, m.totalPro}
                    };

                    var valorProductos = data.Count * 1800000;
                    var valorIva = 0;
                    var valorSubtotal = valorProductos + valorIva;
                    var valorRetencion = 0;
                    var valorTotal = valorSubtotal - valorRetencion;
                    var concepto = "Paquete de transsaciones de la plataforma Paquete de transsaciones de la plataforma Paquete de transsaciones de la plataforma";

                    fields.SetField("ValorProductos", valorProductos.ToString());
                    fields.SetField("ValorIva", valorIva.ToString());
                    fields.SetField("ValorSubtotal", valorSubtotal.ToString());
                    fields.SetField("ValorRetencion", valorRetencion.ToString());
                    fields.SetField("ValorTotal", valorTotal.ToString());
                    fields.SetField("Concepto", concepto);

                    var indexRow = 0;
                    foreach (var row in data)
                    {
                        var indexCell = 0;
                        foreach (var cell in row)
                        {
                            PdfPCell c;
                            if (indexRow != 0)
                            {
                                 c = new PdfPCell(new Phrase(cell, fontTable));
                                c.BorderWidthBottom = 0f;
                                c.BorderWidthTop = 0f;
                                c.BorderWidthLeft = 0f;
                                c.BorderWidthRight = 0f;
                            }
                            else
                            {
                                 c = new PdfPCell(new Phrase(cell, fontHeader));
                                c.BorderWidthTop = 0f;
                                c.BorderWidthLeft = 0f;
                                c.BorderWidthRight = 0f;
                                c.BorderWidthBottom = 1f;
                            }

                            switch (indexCell)
                            {
                                case 0:
                                    c.BorderWidthLeft = 1f;
                                    c.Colspan = 12;
                                    break;
                                case 1:
                                    c.Colspan = 1;
                                    break;
                                case 2:
                                    c.Colspan = 2;
                                    break;
                                case 3:
                                    c.Colspan = 2;
                                    break;
                                case 4:
                                    c.Colspan = 2;
                                    break;
                                case 5:
                                    c.Colspan = 2;
                                    c.BorderWidthRight = 1f;
                                    break;
                            }
                            table.AddCell(c);
                            indexCell++;
                        }
                        indexRow++;
                    }




                    var positions = fields.GetFieldPositions("TableField");
                    if (positions.Count > 0)
                    {
                        var columnText = new ColumnText(stamper.GetOverContent(positions[0].page));
                        columnText.AddElement(table);
                        columnText.SetSimpleColumn(positions[0].position);
                        columnText.Go();
                    }



                    stamper.FormFlattening = true;
                    stamper.Close();


                    pdfReader.Close();

                    ResponseDocument responseDocument = new ResponseDocument()
                    {
                        memoryStream = outputStream,
                        fileName = fileNewName
                    };

                    return responseDocument;
                }

            }
        }


    }
}
