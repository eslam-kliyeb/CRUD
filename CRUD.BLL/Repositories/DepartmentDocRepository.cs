using CRUD.DAL.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRUD.BLL.Repositories
{
    public class DepartmentDocRepository : IDocument
    {
        private int _currentPage = 0;
        private readonly int _pageSize = 20;
        private int _totalPages = 0;
        private IEnumerable<Department> _departments;
        public DepartmentDocRepository(IEnumerable<Department> departments)
        {
            _departments = departments;
            _totalPages = _departments.Count() / _pageSize;
            if (_departments.Count() % _pageSize != 0) _totalPages++;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;
        public void Compose(IDocumentContainer container)
        {
            for (_currentPage = 1; _currentPage <= _totalPages; _currentPage++)
            {
                container
                .Page(page =>
                {
                    page.Size(842, 595);
                    page.Margin(10);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeTable);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            }
        }
        void ComposeHeader(IContainer container)
        {
            using var stream = new FileStream("wwwroot/image/crud.jpg", FileMode.Open);
            container.Row(row =>
            {
                row.ConstantItem(50).TranslateX(500).Image(stream);
                row.RelativeItem().Column(column =>
                {
                    var titleStyle = TextStyle.Default.FontSize(15).SemiBold().FontColor(Colors.Blue.Medium);
                    column.Item().Text($"Departments").Style(titleStyle);
                    DateTime currentTime = DateTime.Now;
                    column.Item().Text(text =>
                    {

                        text.Span("Date : ").SemiBold();
                        text.Span(currentTime.ToString("dddd, dd MMMM yyyy"));
                    });
                    column.Item().Text(text =>
                    {
                        text.Span("Time : ").SemiBold();
                        text.Span(currentTime.ToString("hh:mm tt"));
                    });
                });
            });
        }
        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();

                });
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Code");
                    header.Cell().Element(CellStyle).Text("Name");
                    header.Cell().Element(CellStyle).Text("DateOfCreation");
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderTop(1).BorderColor(Colors.Black);
                    }
                });
                var department = _departments.Skip((_currentPage-1) * _pageSize).Take(_pageSize);
                int Current = 1;
                foreach (var item in department)
                {
                    table.Cell().Element(CellStyle).Text(Current);
                    table.Cell().Element(CellStyle).Text(item.Code);
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).Text(item.DateOfCreation);
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                    Current++;
                }
            });
        }
    }
}
