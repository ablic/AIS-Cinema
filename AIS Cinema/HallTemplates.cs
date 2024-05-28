using AIS_Cinema.Models.HallLayout;

namespace AIS_Cinema
{
    public static class HallTemplates
    {
        public static List<Row> Simple5x5 { get; private set; } = new List<Row>()
        {
            new Row()
            {
                Number = 1,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        Number = 1,
                    },
                    new Seat
                    {
                        Number = 2,
                    },
                    new Seat
                    {
                        Number = 3,
                    },
                    new Seat
                    {
                        Number = 4,
                    },
                    new Seat
                    {
                        Number = 5,
                    },
                }
            },
            new Row()
            {
                Number = 2,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        Number = 1,
                    },
                    new Seat
                    {
                        Number = 2,
                    },
                    new Seat
                    {
                        Number = 3,
                    },
                    new Seat
                    {
                        Number = 4,
                    },
                    new Seat
                    {
                        Number = 5,
                    },
                }
            },
            new Row()
            {
                Number = 3,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        Number = 1,
                    },
                    new Seat
                    {
                        Number = 2,
                    },
                    new Seat
                    {
                        Number = 3,
                    },
                    new Seat
                    {
                        Number = 4,
                    },
                    new Seat
                    {
                        Number = 5,
                    },
                }
            },
            new Row()
            {
                Number = 4,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        Number = 1,
                    },
                    new Seat
                    {
                        Number = 2,
                    },
                    new Seat
                    {
                        Number = 3,
                    },
                    new Seat
                    {
                        Number = 4,
                    },
                    new Seat
                    {
                        Number = 5,
                    },
                }
            },
            new Row()
            {
                Number = 5,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        Number = 1,
                    },
                    new Seat
                    {
                        Number = 2,
                    },
                    new Seat
                    {
                        Number = 3,
                    },
                    new Seat
                    {
                        Number = 4,
                    },
                    new Seat
                    {
                        Number = 5,
                    },
                }
            },
        };
    }
}
