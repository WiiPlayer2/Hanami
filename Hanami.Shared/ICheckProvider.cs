using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hanami.Shared
{
    public interface ICheckProvider : IModule
    {
        event CheckCompletedEventHandler CheckCompleted;

        void Check();
    }
}