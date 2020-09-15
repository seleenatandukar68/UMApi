using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Data;
using UMApi.Helpers;
using UMApi.Models;

namespace UMApi.Services
{
    public interface IUserService
    {
       User Authenticate(string username, string password);
       IEnumerable<User> GetAll();
       User GetById(int id);
       User Create(User user, string password);
       void Update(User user);       
       void Delete(int id);
       void SaveChanges();
    }

    public class UserService : IUserService
    {
        private UMContext _dbContext;

        public UserService(UMContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _dbContext.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_dbContext.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _dbContext.Users.Add(user);
          

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.Include(u => u.Role).ThenInclude(r => r.Subs).ThenInclude(s => s.MainMenu);
        }

        public User GetById(int id)
        {
            User user = _dbContext.Users.Include(u => u.Role).ThenInclude(r => r.Subs).ThenInclude(s => s.MainMenu).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

      

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(User user)
        {
            var oldUser = _dbContext.Users.Find(user.Id);
            if(oldUser == null)
            {
                throw new AppException("User Not Found.");

            }
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            oldUser.PasswordHash = passwordHash;
            oldUser.PasswordSalt = passwordSalt;
            oldUser.RoleId = user.RoleId;
            _dbContext.Entry(oldUser).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var delUser = _dbContext.Users.Find(id);

           if(delUser == null)
            {
                throw new AppException("User Not Found");
            }

            _dbContext.Users.Remove(delUser);
        }
    }

}
