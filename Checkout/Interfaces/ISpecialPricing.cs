namespace Checkout.Interfaces
{
    public interface ISpecialPricing
    {
        decimal CalculateSpecial(int quantityScanned, decimal currentPrice);
    }
}
