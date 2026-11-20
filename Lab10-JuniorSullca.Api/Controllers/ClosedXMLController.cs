using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClosedXMLController : ControllerBase
{
    [HttpPost]
    public void FirstExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Hoja1");
            
            worksheet.Cell(1, 1).Value = "Nombre";
            worksheet.Cell(1, 2).Value = "Edad";
            worksheet.Cell(2, 1).Value = "Juan";
            worksheet.Cell(2, 2).Value = 28;
            
            workbook.SaveAs("C:\\Users\\benja\\Documents\\Example\\archivo.xlsx");
        }
    }
    
    [Route("second")]
    [HttpPost]
    public void SecondExample()
    {
        using (var workbook = new XLWorkbook("C:\\Users\\benja\\Documents\\Example\\archivo.xlsx"))
        {
            var worksheet = workbook.Worksheet(1);
            
            worksheet.Cell(2, 2).Value = 39;

            workbook.Save();
        }
    }
    
    [Route("third")]
    [HttpPost]
    public void ThirdExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Datos");
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nombre";
            worksheet.Cell(1, 3).Value = "Edad";

            worksheet.Cell(2, 1).Value = 1;
            worksheet.Cell(2, 2).Value = "Juan";
            worksheet.Cell(2, 3).Value = 28;
            
            worksheet.Cell(3, 1).Value = 2;
            worksheet.Cell(3, 2).Value = "María";
            worksheet.Cell(3, 3).Value = 34;

            var range = worksheet.Range("A1:C3");
            range.CreateTable();
            
            workbook.SaveAs("C:\\Users\\benja\\Documents\\Example\\tabla.xlsx");
        }
    }
    
    [Route("fourth")]
    [HttpPost]
    public void FourthExample()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Estilos");

            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.Cyan;
            headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Nombre";
            worksheet.Cell(1, 3).Value = "Edad";

            worksheet.Cell(2, 1).Value = 1;
            worksheet.Cell(2, 2).Value = "Juan";
            worksheet.Cell(2, 3).Value = 28;
            
            workbook.SaveAs("C:\\Users\\benja\\Documents\\Example\\tabla_con_estilos.xlsx");
        }
    }
}






