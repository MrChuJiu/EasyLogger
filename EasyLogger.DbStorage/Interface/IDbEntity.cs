using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.DbStorage.Interface
{
    public interface IDbEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
