using BookingApp.Model;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
        public class CheckpointsComponent : IComponent
        {
            private List<Checkpoint> _checkpoints;
            public CheckpointsComponent(List<Checkpoint> pointsOfInterest)
            {
                _checkpoints = pointsOfInterest;
            }
            public void Compose(IContainer container)
            {
                container.Column(column => {
                    column.Spacing(2);

                    column.Item().BorderBottom(1).PaddingBottom(5).Text("Tour checkpoints:").SemiBold();

                    foreach (var point in _checkpoints)
                        column.Item().Text($"- {point.Name}");
                });
            }
        }
    }

