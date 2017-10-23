using System;
using System.Collections.Generic;
using System.Text;

namespace Products.NetCore.Service.Helpers.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception exception) : base(message, exception) { }
    }
}
