using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface IAuthorService
    {
        IEnumerable<Author> ListAuthors();

        Author Get(int id);

        Author FindAuthor(AuthorDTO authorDto);

        bool Add(AuthorDTO author);

        bool Delete(int id);

        bool Update(int id, AuthorDTO authorDto);
    }
}