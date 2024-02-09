namespace Infrastructure.ExternalServices
{
    public interface IDiscountService
    {
        Task<decimal> GetDiscountAsync(string productId);
    }

}
