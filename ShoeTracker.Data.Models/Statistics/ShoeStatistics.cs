namespace ShoeTracker.Data.Models.Statistics
{
    public class ShoeStatistics
    {
        public int TotalShoes { get; set; }
        public double TotalDistance { get; set; }
        public int TotalRuns { get; set; }
        public int ShoesNeedingReplacement { get; set; }
        public double AverageDistancePerShoe { get; set; }

    }
}
