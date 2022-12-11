namespace AoC2022.Models
{
    public class Strategy
    {
        public int Points { get; set; }

        public Strategy(Round opponent, string strategy)
        {
            Round me;
            switch (strategy)
            {
                case "Y":
                    me = new Round(opponent.ColumnCode);
                    Points = me.ShapePoints + 3;
                    break;
                case "X":
                    me = new Round(shape: opponent.WinsAgainst);
                    Points = me.ShapePoints;
                    break;
                case "Z":
                    me = new Round(shape: opponent.LosesAgainst);
                    Points = me.ShapePoints + 6;
                    break;
            }
        }
    }
}