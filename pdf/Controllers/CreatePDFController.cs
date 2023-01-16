using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pdf.Models;
using pdf.Util;

namespace pdf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePDFController : ControllerBase
    {

        [HttpPost]
        public IActionResult GenerateDocumentByTemplate(FacturaModel m)
        {
            if(m == null)
            {
                throw new ArgumentException(nameof(m));
            }
            GeneratePDF generatePDF = new GeneratePDF();
            ResponseDocument document = new ResponseDocument();
            document = generatePDF.GenerateInvestorDocument(m);
            byte[] bytesFile = document.memoryStream.ToArray();

            return File(bytesFile, "application/pdf", document.fileName);

            //if (string.IsNullOrWhiteSpace(newDocumentFileName))
            //    return BadRequest("Un error al crear el archivo.");
            //return Ok(newDocumentFileName);
        }


    }
}
