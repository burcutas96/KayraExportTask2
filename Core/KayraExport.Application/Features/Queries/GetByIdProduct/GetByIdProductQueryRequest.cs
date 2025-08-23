using KayraExport.Application.Abstractions.Cache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Features.Queries.GetById
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>, ICacheableQuery
    {
        public int ProductId { get; set; }

        public string GetCacheKey() => $"{nameof(GetByIdProductQueryRequest)}_{ProductId}";
    } 
}
