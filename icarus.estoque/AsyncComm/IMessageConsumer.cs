using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.estoque.Models;

namespace icarus.estoque.AsyncComm
{
    public interface IMessageConsumer
    {
        ConsumerDTO consumeMessage();
    }
}