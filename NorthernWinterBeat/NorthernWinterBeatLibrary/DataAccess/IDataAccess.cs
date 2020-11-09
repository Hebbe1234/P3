using System;
using System.Collections.Generic;
using System.Text;

namespace NorthernWinterBeatLibrary.DataAccess
{
    public interface IDataAccess
    {
        public void Save();
        public List<T> Retrieve<T>();

        public void Add<T>(T input);
    }
}


