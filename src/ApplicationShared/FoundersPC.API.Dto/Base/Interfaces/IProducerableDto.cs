namespace FoundersPC.API.Dto.Base.Interfaces
{
    internal interface IProducerableDto : IProducerIdentifiable
    {
        ProducerReadDto Producer { get; set; }
    }
}