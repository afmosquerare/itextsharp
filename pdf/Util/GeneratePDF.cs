using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using pdf.Models;

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

            string fullExistingPath = filePath+fileNameExisting;


            using (var existingFileStream = new FileStream(fullExistingPath, FileMode.Open))

            using(MemoryStream outputStream =  new MemoryStream())
            {
                using (var newFileStream = new FileStream(fullNewPath, FileMode.Create))
                {
                    var pdfReader = new PdfReader(existingFileStream);
                    var stamper = new PdfStamper(pdfReader, outputStream);

                    AcroFields fields = stamper.AcroFields;


                    var plazo = ((m.FechaVence - m.FechaFactura).TotalDays).ToString() + " Dias";


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
