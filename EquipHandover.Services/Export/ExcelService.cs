using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using EquipHandover.Services.Contracts.Export;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Extensions;

namespace EquipHandover.Services.Export;

/// <inheritdoc cref="IExcelService"/>
public class ExcelService : IExcelService, IServiceAnchor
{
    Stream IExcelService.Export(DocumentModel document, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();

        using (var spreadsheetDocument = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
        {
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = spreadsheetDocument.WorkbookPart?.Workbook.AppendChild(new Sheets());
            sheets?.Append(new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart?.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Акт о приеме-передачи оборудования"
            });

            uint rowIndex = 1;

            AddRow(sheetData, rowIndex++, "", $"г. {document.City}", "", $"{document.SignatureNumber} г.");
            AddRow(sheetData, rowIndex++, "");
            AddRow(sheetData, rowIndex++, "", $"«{document.Sender.Enterprise}» в лице  {document.Sender.FullName}");
            AddRow(sheetData, rowIndex++, "",
                $"«{document.Receiver.Enterprise}», и в лице  {document.Receiver.FullName}");
            AddRow(sheetData, rowIndex++, "", $"составили настоящий акт в том, что «{document.Sender.Enterprise}»");
            AddRow(sheetData, rowIndex++, "",
                $"передает, а «{document.Receiver.Enterprise}»  принимает следующее оборудование");
            AddRow(sheetData, rowIndex++, "");
            AddTable(sheetData, ref rowIndex, document);
            AddRow(sheetData, rowIndex++, "",
                $"Оборудование передается в аренду на {document.RentalDate.Month} месяцев (лет) согласно договора");
            AddRow(sheetData, rowIndex++, "", 
                $"№ {document.Id.ToString()[..8]} ", "", $" от {document.RentalDate.Year} г.");
            AddRow(sheetData, rowIndex++, "", "Комплектность проверена.");
            AddRow(sheetData, rowIndex++, "",
                $"«{document.Receiver.Enterprise}» по качеству и составу принятого оборудования");
            AddRow(sheetData, rowIndex++, "", "претензий не имеет.");
            AddRow(sheetData, rowIndex++, "");
            AddRow(sheetData, rowIndex++, "",
                $"Сдал    «{document.Sender.Enterprise}»     {document.Sender.FullName}");
            AddRow(sheetData, rowIndex, "",
                $"Принял    «{document.Receiver.Enterprise}»     {document.Receiver.FullName}");

            workbookPart.Workbook.Save();
        }

        stream.Position = 0;
        return stream;
    }
    
    private static void AddRow(SheetData sheetData, uint rowIndex, params string[] columns)
    {
        var row = new Row() { RowIndex = rowIndex };
        foreach (var column in columns)
        {
            var cell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(column)
            };
            row.Append(cell);
        }
        
        sheetData.Append(row);
    }
    
    private static void AddTable(SheetData sheetData, ref uint rowIndex, DocumentModel document)
    {
        AddRow(sheetData, rowIndex++, 
            "№№", 
            "Наименование оборудования", 
            "Год выпуска", 
            "Заводской номер");

        var numberEquipment = 1;
        foreach (var equipment in document.Equipment)
        {
            AddRow(sheetData, rowIndex++,
                $"{numberEquipment.ToString()}",
                $"{equipment.Name}",
                $"{equipment.ManufacturedYear}",
                $"{equipment.SerialNumber}");
            numberEquipment++;
        }
        AddRow(sheetData, rowIndex++, "");
    }
}