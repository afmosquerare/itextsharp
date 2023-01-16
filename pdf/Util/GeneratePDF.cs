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

            string fileNameExisting = @"Ejemplo.pdf";
            
            string fileNewName = @"Ejemplo_" + m.Cliente +"_"+ m.IdCliente.ToString().Trim() + ".pdf";

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




                    fields.SetField("Nombre", m.Nombre);
                    fields.SetField("Apellido", m.Apellido);
                    fields.SetField("Documento", m.Documento);




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
