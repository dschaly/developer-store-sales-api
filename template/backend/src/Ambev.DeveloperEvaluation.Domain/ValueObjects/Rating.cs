namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed record Rating
{
    public decimal Rate { get; init; }
    public int Count { get; init; }

    public Rating(decimal rate, int count)
    {
        if (rate < 0 || rate > 5) throw new ArgumentException("Rate must be between 0 and 5.");
        if (count < 0) throw new ArgumentException("Count cannot be negative.");

        Rate = rate;
        Count = count;
    }
}
