using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Infrastructure.Models;


namespace Infrastructure.ExternalServices
{
    public class ExternalDiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExternalDiscountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> GetDiscountAsync(string productId)
        {
            var apiUrl = _configuration["ExternalDiscountService:ApiUrl"];

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var discountString = await response.Content.ReadAsStringAsync();
                    var discountObject = JsonConvert.DeserializeObject<List<Discount>>(discountString);

                    var productDiscount = discountObject.FirstOrDefault(d => d.id == productId);

                    if (productDiscount != null && decimal.TryParse(productDiscount.discount, out var discount))
                    {
                        return discount;
                    }

                }
            }
            catch (HttpRequestException ex)
            {
            }
            catch (Exception ex)
            {

            }

            return 0m;
        }

    }


}
