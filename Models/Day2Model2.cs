namespace AoC2022.Models
{
    public class Day2Model2
    {
        public int Points { get; set; }

        public Day2Model2(Day2Model opponent, string strategy)
        {
            Day2Model me;
            switch (strategy)
            {
                case "Y":
                    me = new Day2Model(opponent.ColumnCode);
                    Points = me.ShapePoints + 3;
                    break;
                case "X":
                    me = new Day2Model(shape: opponent.WinsAgainst);
                    Points = me.ShapePoints;
                    break;
                case "Z":
                    me = new Day2Model(shape: opponent.LosesAgainst);
                    Points = me.ShapePoints + 6;
                    break;
            }
        }
    }
}