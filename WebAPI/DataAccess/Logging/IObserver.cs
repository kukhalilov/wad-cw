using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Logging
{
    public interface IObserver
    {
        void Update(string message);
    }
}
