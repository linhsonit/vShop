using MediatR;
using ProductService.Data.Model;

namespace ProductService.Applications.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public Product Product { get; set; }
    }
}
