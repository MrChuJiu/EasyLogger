using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.DbStorage.Interface
{
    public interface IPartitionDbTableFactory
    {
        void DbTableCreate(string path, bool isBaseDb);
    }
}
