namespace FoundersPC.Application.Base.Interfaces
{
    public interface IProducerableDto : IProducerIdentiable
    {
        ProducerReadDto Producer { get; set; }
    }
}