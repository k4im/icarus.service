using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icarus.estoque.AsyncComm
{
    public interface IMessageConsumer
    {
        void consumeMessage();
    }
}