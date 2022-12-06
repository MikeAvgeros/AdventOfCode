namespace AoC2022.Models
{
    public class Day2Model
    {
        public string ColumnCode { get; set; }
        public string Shape { get; set; }
        public int ShapePoints { get; set; }
        public string WinsAgainst { get; set; }
        public string LosesAgainst { get; set; }

        public Day2Model(string columnCode = "", string shape = "")
        {
            if (!string.IsNullOrEmpty(columnCode))
            {
                ColumnCode = columnCode;
                switch (columnCode)
                {
                    case "A" or "X":
                        Shape = "Rock";
                        ShapePoints = 1;
                        WinsAgainst = "Scissors";
                        LosesAgainst = "Paper";
                        break;
                    case "B" or "Y":
                        Shape = "Paper";
                        ShapePoints = 2;
                        WinsAgainst = "Rock";
                        LosesAgainst = "Scissors";
                        break;
                    case "C" or "Z":
                        Shape = "Scissors";
                        ShapePoints = 3;
                        WinsAgainst = "Paper";
                        LosesAgainst = "Rock";
                        break;
                }
            }
            
            if (!string.IsNullOrEmpty(shape))
            {
                Shape = shape;
                switch (shape)
                {
                    case "Rock":
                        ColumnCode = "A";
                        ShapePoints = 1;
                        WinsAgainst = "Scissors";
                        LosesAgainst = "Paper";
                        break;
                    case "Paper":
                        ColumnCode = "B";
                        ShapePoints = 2;
                        WinsAgainst = "Rock";
                        LosesAgainst = "Scissors";
                        break;
                    case "Scissors":
                        ColumnCode = "C";
                        ShapePoints = 3;
                        WinsAgainst = "Paper";
                        LosesAgainst = "Rock";
                        break;
                }
            }
            
        }
    }
}