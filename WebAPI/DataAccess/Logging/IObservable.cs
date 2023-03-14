using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Logging
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string message);
    }
}
