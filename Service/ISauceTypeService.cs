using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface ISauceTypeService
    {
        IEnumerable<SauceType> ListSauceTypes();

        SauceType Get(int id);

        bool Add(SauceTypeDTO sauceTypes);

        bool Delete(int id);

        bool Update(int id, SauceTypeDTO sauceTypes);
    }
}
