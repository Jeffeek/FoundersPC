namespace FoundersPC.API.Dto.Base.Interfaces
{
    public interface IProducerableDto : IProducerIdentifiable
    {
        ProducerReadDto Producer { get; set; }
    }
}