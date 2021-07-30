using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public List<User> users = new List<User>()
        {
            new User(){ UserId = 1, FirstName ="Indira", LastName= "Shahi", Password = "12345", ConfirmPassword = "12345" ,Email = "indirashahi@gmail.com"  }
        };

        public List<User> GetUsers()
        {
            return users;
        }

        public User RegisterNewUser(User newUser)
        {
            newUser.UserId = users.Count + 1;
            users.Add(newUser);
            return newUser;
        }

        public User UserRegister(User newUser)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int userid)
        {
            var user = users.Find(user => user.UserId == userid);
            return user;
        }

        public User GetUser(string email)
        {
            var user = users.Find(user => user.Email == email);
            return user;
        }

        public User UpdateUser(User user)
        {
            User existingUser = GetUser(user.UserId);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.ConfirmPassword = user.Password;
            return existingUser;
        }

        private User GetUser(long userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            return users.Remove(user);
        }

        public User UserLogin(Login login)
        {
            var user = users.Find(x => x.Email == login.Email && x.Password == login.Password);
            return user;
        }
        public User ForgotPassword(string email)
        {
            User existingUser = users.Find(user => user.Email == email);
            if (existingUser != null)
                return existingUser;
            return null;
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public User ResetPassword(User user, string newPassword)
        {
            user.Password = newPassword;
            user.ConfirmPassword = newPassword;

            return user;
        }

        public User ResetPassword(ResetPassword reset)
        {
            throw new NotImplementedException();
        }

    }
}
    

