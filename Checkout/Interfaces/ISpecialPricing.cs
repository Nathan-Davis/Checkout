namespace Checkout.Interfaces
{
    public interface ISpecialPricing
    {
        decimal CalculateSpecial(decimal units, decimal currentPrice);
    }
}
