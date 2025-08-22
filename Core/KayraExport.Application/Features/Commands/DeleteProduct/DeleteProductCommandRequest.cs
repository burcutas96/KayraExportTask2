using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public int ProductId { get; set; }
    }
}
