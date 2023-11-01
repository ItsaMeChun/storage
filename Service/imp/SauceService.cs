using AutoMapper;
using hcode.DTO;
using hcode.Entity;
using hcode.Repository;

namespace hcode.Service.imp
{
    public class SauceService : ISauceService
    {
        private readonly IRepository<Sauce> _sauceRepository;
        private readonly IMapper _mapper;

        public SauceService(IRepository<Sauce> sauceRepository, IMapper mapper)
        {
            this._sauceRepository = sauceRepository;
            this._mapper = mapper;
        }
        public bool Add(SauceDTO sauce)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            //return _sauceRepository.Delete(id);
            var sauce = _sauceRepository.FindById(id);
            if(sauce == null)
            {
                // help, i got task
                return false;
            }

            return true;
        }

        public Sauce Get(int id)
        {
            return _sauceRepository.FindById(id);
        }

        public IEnumerable<Sauce> ListSauces()
        {
            return _sauceRepository.GetAll();
        }

        public bool Update(int id, SauceDTO sauce)
        {
            throw new NotImplementedException();
        }

        public Sauce FindSauce(SauceDTO sauceDto)
        {
            throw new NotImplementedException();
        }
    }
}
