namespace RineaR.MadeHighlow.Directors.Environments
{
    public static class RandomComparer
    {
        public static int CompareRandom(this IRandomGenerator generator)
        {
            return generator.Range(-1f, 1f).CompareTo(0f);
        }
    }
}
