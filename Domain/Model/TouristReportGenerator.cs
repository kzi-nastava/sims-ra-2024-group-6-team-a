using BookingApp.DTOs;
using BookingApp.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;

namespace BookingApp.Domain.Model
{
    public class TouristReportGenerator : IDocument
    {
        private DateTime _currentDateTime;
        private List<Checkpoint> _checkpoints;
        private List<TourGuests> _guests;
        private TourScheduleDTO _tourDetails;
        private Tourist _user;
        
        public TouristReportGenerator(DateTime currentDate, TourScheduleDTO schedule, Tourist tourist, List<Checkpoint> checkpoints, List<TourGuests> guests)
        {
            _currentDateTime = currentDate;
            _tourDetails = schedule;
            _checkpoints = checkpoints;
            _guests = guests;
            _user = tourist;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }


        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Green.Darken3);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Tour reservation report").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"{_currentDateTime:d}");
                    });

                    column.Item().Text(text => {
                        text.Span("Issued by: ").SemiBold();
                        text.Span($"{_user.Name} {_user.Surname}");
                    });
                });

                row.ConstantItem(200).AlignRight().Height(50).Image(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images/logo.png")));
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Component(new TourDetailsComponent(_tourDetails));
                    row.ConstantItem(50);
                    row.RelativeItem().Component(new CheckpointsComponent(_checkpoints));
                });

                column.Item().PaddingTop(25).Element(ComposeComments);

                column.Spacing(5);

                column.Item().Element(ComposeTable);
            });
        }
        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Description").FontSize(14).Bold();
                column.Item().Text(_tourDetails.Description);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Name");
                    header.Cell().Element(CellStyle).AlignRight().Text("Surname");
                    header.Cell().Element(CellStyle).AlignRight().Text("Age");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                int counter = 0;
                foreach (var person in _guests)
                {
                    table.Cell().Element(CellStyle).Text(++counter);
                    table.Cell().Element(CellStyle).Text(person.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text(person.Surname);
                    table.Cell().Element(CellStyle).AlignRight().Text(person.Age);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
    }
}
