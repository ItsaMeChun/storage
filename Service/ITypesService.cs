using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface ITypesService
    {
        IEnumerable<Types> ListTypes();

        Types Get(int id);

        bool Add(TypesDTO type);

        bool Delete(int id);

        bool Update(int id, TypesDTO type);
    }
}
