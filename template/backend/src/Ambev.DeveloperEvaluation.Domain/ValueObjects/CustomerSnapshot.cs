using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class CustomerSnapshot
    {
        public Guid ExternalCustomerId { get; }
        public string Name { get; }

        public CustomerSnapshot(Guid externalCustomerId, string name)
        {
            ExternalCustomerId = externalCustomerId;
            Name = name;
        }
    }
}
