using icarus.projetos.models.ModelsShared;

namespace icarus.projetos.AsyncComm
{
    public interface IMessageBusService
    {
        void publishNewProjeto(PublishProject evento);
    }
}