namespace IFLike.Dto
{
    public abstract class DtoBaseId<T>: DtoBase
    {
        public T Id { get; set; }
    }
}
