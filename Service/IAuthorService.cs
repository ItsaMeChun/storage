using hcode.DTO;
using hcode.Entity;

namespace hcode.Service
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDTO> ListAuthors();

        Author Get(int id);

        bool Add(Author author);

        bool Delete(int id);

        bool Update(int id, AuthorDTO authorDto);
    }
}