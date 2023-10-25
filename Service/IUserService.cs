using System.Linq.Expressions;
using hcode.DTO;
using hcode.Entity;


namespace hcode.Service
{
    public interface IUserService
    {
        bool Add(User author);

        bool Delete(int id);

        User Get(int id);

        IEnumerable<User> GetAll();

        bool Update(int authorId, UserDTO authorDto);

        User FindUser(UserDTO authorDto);

        User CheckUserLogin(UserLogin authorDto);

        public AuthResponse GenerateToken(User author);
    }
}
