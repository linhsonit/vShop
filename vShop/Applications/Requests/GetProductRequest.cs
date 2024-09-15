using MediatR;
using ProductService.Data.Model;

namespace ProductService.Applications.Requests
{
    public class GetProductRequest : IRequest<Product?>
    {
        public int ProductId { get; set; }
    }
}
