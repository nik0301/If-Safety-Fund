using IFLike.Domain;

namespace IFLike.DAL.Interfaces
{
    public interface IRepositoryBase<T, TId> where T : EntityBaseId<TId>
    {
        T GetById(TId id);
        void Save();
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
    }
}
