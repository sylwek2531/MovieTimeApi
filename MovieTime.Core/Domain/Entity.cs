using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public DateTime CreateAt { get; protected set; }

        public DateTime UpdateAt { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }
    }
}
