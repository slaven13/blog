using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Entities
{
    public class BaseEntity: IEntity
    {
        public long Id { get; set; }
    }
}
