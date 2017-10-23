using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.NetCore.WebAPI.DTOs
{
    public class CollectionDTO<T>
    {
        public IEnumerable<T> Items { get; set; }

    }
}
