namespace AoC2022.Models
{
    public class Move
    {
        public int Amount { get; set; }
        public int From { get; set; } 
        public int To { get; set; }

        public Move(int amount, int from, int to)
        {
            Amount = amount;
            From = from;
            To = to;
        }
    }
}