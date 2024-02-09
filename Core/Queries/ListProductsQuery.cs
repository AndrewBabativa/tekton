using MediatR;
using Infrastructure.Models;

namespace Core.Queries
{
    /// <summary>
    /// Represents a query for retrieving a list of products.
    /// </summary>
    public class ListProductsQuery : IRequest<List<Product>>
    {

    }
}
