using System;
using System.Collections.Generic;
using System.Text;

namespace Dashagram.Domain.Models.Entities
{
    public abstract record Entity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
