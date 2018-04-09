using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessApp.Portable
{
    public interface IMessage
    {
        void longtime(string message);
        void shorttime(string message);
    }
}
