namespace BingSearcher;

public static class Extensions
{
    private static readonly Random _random = new Random();
    public static async Task RandomDelay(this Task task, int minDelay = 1000, int maxDelay = 2000)
    {
        var delay = _random.Next(minDelay, maxDelay);
        await Task.Delay(delay);
    }
}
