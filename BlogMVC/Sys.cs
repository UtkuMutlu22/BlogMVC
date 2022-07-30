using ClosedXML.Excel;

public static class Sys
{
    private static readonly Dictionary<string, string> mimeToIcon = new Dictionary<string, string>{
            {"text/plain", "fa-file"},
            {"application/vnd.openxmlformats-officedocument.wordprocessingml.document" ,"fa-file-word"},
            {"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ,"fa-file-excel"},
            {"application/vnd.openxmlformats-officedocument.presentationml.presentation" ,"fa-file-powerpoint"},
            {"application/x-abiword" ,"fa-file-word"},
            {"application/vnd.ms-excel" ,"fa-file-excel"},
            {"application/vnd.ms-powerpoint" ,"fa-file-powerpoint"},
            {"image/png" ,"fa-file-image"},
            {"image/jpeg" ,"fa-file-image"},
            {"text/csv" ,"fa-file-csv"},
            {"application/pdf" ,"fa-file-pdf"},
            {"audio/acc" ,"fa-headphones"},
            {"application/vnd.rar" ,"fa-file-zipper"},
            {"application/msword" ,"fa-file-word"},
            {"video/x-msvideo" ,"fa-file-video"},
            {"application/x-tar" ,"fa-file-zipper"},
            {"application/zip" ,"fa-file-zipper"},
            {"application/gzip" ,"fa-file-zipper"},
        };

    public static WebApplication? App { get; set; }

    public static byte[] ToExcelExportFile<T>(this IEnumerable<T> query, string sheetName)
    {
        int rowCounter = 1;
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(sheetName);

        var list = query
            .ToList();

        if (list.Any())
        {
            list
                .First()!
                .GetType()
                .GetProperties()
                .Select((p, i) => new { Index = i, Name = p.Name })
                .ToList()
                .ForEach(p => worksheet.Cell(1, p.Index + 1).Value = p.Name);

            query
                .ToList()
                .ForEach(p =>
                {
                    ++rowCounter;
                    p
                        .GetType()
                        .GetProperties()
                        .Select((q, i) => new { Property = q, Index = i })
                        .ToList()
                        .ForEach(q => worksheet.Cell(rowCounter, q.Index + 1).Value = p.GetType().GetProperty(q.Property.Name)!.GetValue(p)!.ToString());
                });


        }
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static string MimeToIcon(string mimeType) => mimeToIcon.ContainsKey(mimeType) ? mimeToIcon[mimeType] : "fa-file";


}