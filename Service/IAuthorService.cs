using System.Linq.Expressions;
using hcode.DTO;
using hcode.Entity;


namespace hcode.Service
{
    public interface IAuthorService
    {
        bool Add(Author author);
        bool Delete(int id);
        Author Get(int id);
        IEnumerable<Author> GetAll();
        bool Update(int authorId, AuthorDTO authorDto);
        Author FindUser(AuthorDTO authorDto);
        Author CheckUserLogin(AuthorLogin authorDto);
        public AuthResponse GenerateToken(Author author);
    }
}
