using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset CreatesAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            CreatesAt = DateTime.Now;
        }
    }
}
