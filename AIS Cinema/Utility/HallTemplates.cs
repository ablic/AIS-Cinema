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

        public static List<Row> Complex8 { get; private set; } = new List<Row>()
        {
            new Row() // 6
            {
                Number = 1,
                Seats = new List<Seat>
                {
                    new Seat
                    {
                        LeftGap = .7f,
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
                    new Seat
                    {
                        Number = 6,
                    },
                }
            },
            new Row() // 7
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                }
            },
            new Row() // 8
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                    new Seat
                    {
                        Number = 8,
                    },
                }
            },
            new Row() // 8
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
                        RightGap = .5f,
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        LeftGap = .5f,
                        Number = 7,
                    },
                    new Seat
                    {
                        Number = 8,
                    },
                }
            },
            new Row() // 8
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                    new Seat
                    {
                        Number = 8,
                    },
                }
            },
            new Row() // 8
            {
                Number = 6,
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                    new Seat
                    {
                        Number = 8,
                    },
                }
            },
            new Row() // 7
            {
                Number = 7,
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                }
            },
            new Row() // 7
            {
                Number = 8,
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
                    new Seat
                    {
                        Number = 6,
                    },
                    new Seat
                    {
                        Number = 7,
                    },
                }
            },
        };

        public static List<Row> Triangle { get; private set; } = new List<Row>()
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
