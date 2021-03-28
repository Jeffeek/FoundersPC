namespace FoundersPC.API.Dto.Base.Interfaces
{
    public interface IProducerableDto : IProducerIdentiable
    {
        ProducerReadDto Producer { get; set; }
    }
}