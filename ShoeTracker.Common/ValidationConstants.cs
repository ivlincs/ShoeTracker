namespace ShoeTracker.Common
{
    public static class ValidationConstants
    {
        public static class Shoe
        {
            public const int BrandMaxLength = 50;
            public const int ModelMaxLength = 50;
            public const double MinDistance = 0;
            public const double MaxDistance = 10000;
        }

        public static class Run
        {
            public const double MinDistance = 0.1;
            public const double MaxDistance = 200;
        }

        public static class Category
        {
            public const int NameMaxLength = 30;
        }
    }
}
