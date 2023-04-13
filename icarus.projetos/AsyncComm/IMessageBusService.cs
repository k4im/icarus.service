using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models;

namespace icarus.projetos.AsyncComm
{
    public interface IMessageBusService
    {
        void publishNewProjeto(ProjectDTO evento);
    }
}