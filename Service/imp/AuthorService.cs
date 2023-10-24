using hcode.DTO;
using hcode.Entity;
using hcode.Repository;
using hcode.Utils;

namespace hcode.Service.imp
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;

        private readonly IConfiguration _configuration;
        
        private readonly UserSecurity userSecurity;

        //private readonly Dotenv _env;

        public AuthorService(IRepository<Author> authorRepository, IConfiguration configuration)
        {
            this._authorRepository = authorRepository;
            this._configuration = configuration;
            userSecurity = new UserSecurity(_configuration);
        }

        public bool Add(Author author)
        {
            author.Password = userSecurity.MD5Hash(author.Password);
            var result = _authorRepository.Add(author);
            return result;
        }

        public bool Update(int authorId, AuthorDTO authorDto)
        {
            var author = Get(authorId);
            //lazy to mapp
            author.Password = userSecurity.MD5Hash(authorDto.Password);
            author.UserName = authorDto.UserName;
            author.Email = authorDto.Email;
            return _authorRepository.Update(author);
        }

        public bool Delete(int id)
        {
            return _authorRepository.Delete(id);
        }

        public Author Get(int id)
        {
            return _authorRepository.FindById(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll().OrderBy(p => p.Id);
        }

        public Author FindUser(AuthorDTO authorDto)
        {
            return _authorRepository.Find(user => user.UserName.TrimEnd().ToUpper() == authorDto
            .UserName.TrimEnd().ToUpper() || user.Email.TrimEnd().ToUpper() == authorDto
            .Email.TrimEnd().ToUpper());
        }

        public Author CheckUserLogin(AuthorLogin authorDto)
        {
            Author user = _authorRepository.Find(user => user.Email.TrimEnd().ToUpper() == authorDto
            .Email.TrimEnd().ToUpper());
            if (user == null)
            {
                return null;
            }
            bool checkUser = userSecurity.CompareMD5Hash(authorDto.Password, user.Password);
            if (checkUser)
            {
                return user;
            }
            return null;
        }

        public AuthResponse GenerateToken(Author author)
        {
            AuthResponse authResponse = new AuthResponse { AccessToken = userSecurity.CreateToken(author), ExpireDate = DateTime.UtcNow.AddMinutes(15) };
            return authResponse;
        }
    }
}
