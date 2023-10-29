using AutoMapper;
using hcode.DTO;
using hcode.Entity;
using hcode.Repository;

namespace hcode.Service.imp
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> authorRepository, IMapper mapper)
        {
            this._authorRepository = authorRepository;
            this._mapper = mapper;
        }
        public bool Add(AuthorDTO author)
        {
            var authorMapper = _mapper.Map<Author>(author);
            return _authorRepository.Add(authorMapper) ? true : false;
        }

        public bool Delete(int id)
        {
            return _authorRepository.Delete(id);
        }

        public IEnumerable<Author> ListAuthors()
        {
            return _authorRepository.GetAll();
        }
        public Author Get(int id)
        {
            return _authorRepository.FindById(id);
        }

        public bool Update(int id, AuthorDTO authorDto)
        {
            var existingAuthor = _authorRepository.FindById(id);
            if (existingAuthor == null)
            {
                return false;
            }
            _mapper.Map(authorDto, existingAuthor);
            return _authorRepository.Update(existingAuthor);
        }
        public Author FindAuthor(AuthorDTO authorDto)
        {
            return _authorRepository.Find(author => author.Name.ToUpper() == authorDto.Name.ToUpper());
        }
    }
}
