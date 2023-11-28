using AutoMapper;
using hcode.DTO;
using hcode.Entity;
using hcode.Repository;
using hcode.Utils;

namespace hcode.Service.imp
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserSecurity userSecurity;

        public UserService(IRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
            this._mapper = mapper;
            userSecurity = new UserSecurity(_configuration);
        }

        public bool Add(UserDTO user)
        {
            user.Password = userSecurity.MD5Hash(user.Password);
            var userMapper = _mapper.Map<User>(user);
            var result = _userRepository.Add(userMapper);
            return result;
        }

        public bool Update(int userId, UserDTO userDto)
        {
            var user = Get(userId);
            //lazy to mapp
            user.Password = userSecurity.MD5Hash(userDto.Password);
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            return _userRepository.Update(user);
        }

        public bool Delete(int id)
        {
            return _userRepository.Delete(id);
        }

        public User Get(int id)
        {
            return _userRepository.FindById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll().OrderBy(p => p.Id);
        }

        public User FindUser(UserDTO userDto)
        {
            return _userRepository.Find(user => user.UserName.TrimEnd().ToUpper() == userDto
            .UserName.TrimEnd().ToUpper() || user.Email.TrimEnd().ToUpper() == userDto
            .Email.TrimEnd().ToUpper());
        }

        public User CheckUserLogin(UserLogin userDto)
        {
            User user = _userRepository.Find(user => user.Email.TrimEnd().ToUpper() == userDto
            .Email.TrimEnd().ToUpper());
            if (user == null)
            {
                return null;
            }
            bool checkUser = userSecurity.CompareMD5Hash(userDto.Password, user.Password);
            if (checkUser)
            {
                return user;
            }
            return null;
        }

        public AuthResponse GenerateToken(User user)
        {
            AuthResponse authResponse = new AuthResponse { AccessToken = userSecurity.CreateToken(user), ExpireDate = DateTime.UtcNow.AddMinutes(15) };
            return authResponse;
        }
    }
}
