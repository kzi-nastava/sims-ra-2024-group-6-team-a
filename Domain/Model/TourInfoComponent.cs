using BookingApp.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{

    internal class TourDetailsComponent : IComponent
    {
        private TourScheduleDTO _tourDetails;
        public TourDetailsComponent(TourScheduleDTO tourDetails)
        {
            _tourDetails = tourDetails;
        }
        public void Compose(IContainer container)
        {
            container.Column(column => {
                column.Spacing(2);

                column.Item().BorderBottom(1).PaddingBottom(5).Text("Information").Bold();

                column.Item().Text($"Name: {_tourDetails.TourName}");
                column.Item().Text($"Location: {_tourDetails.City} - {_tourDetails.State}");
                column.Item().Text($"Language: {_tourDetails.TourLanguage}");
                column.Item().Text($"Date: {_tourDetails.Start:g}");
                column.Item().Text($"Duration: {_tourDetails.Duration}h");
            });
        }
    }
}

