using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface ISauceService
    {
        IEnumerable<Sauce> ListSauces();

        Sauce Get(int id);

        bool Add(SauceDTO sauce);

        bool Delete(int id);

        bool Update(int id, SauceDTO sauce);

        Sauce FindSauce(SauceDTO sauceDto);

        public List<SauceDTO> uploadImage(IFormFile[] files);


    }
}
