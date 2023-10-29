using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface ISauceHistoryService
    {
        IEnumerable<SauceHistory> ListSauceHistories();

        SauceHistory Get(int id);

        bool Add(SauceHistoryDTO sauceHistory);

        bool Delete(int id);

        bool Update(int id, SauceHistoryDTO sauceHistory);
    }
}
