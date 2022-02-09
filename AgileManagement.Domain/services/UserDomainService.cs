using AgileManagement.Core;
using AgileManagement.Core.domain;
using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
      

        public UserDomainService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public ApplicationUserResult CreateUser(string email, string password)
        {            
            var result = new ApplicationUserResult() { IsSucceeded = true };

            // email unique mi
            var emailExists = _userRepository.GetQuery().FirstOrDefault(x => x.Email == email) == null ? false:true;

            if (emailExists)
            {
                result.Errors.Add("Aynı hesaptan sistemde mevcut!");
            } 
            else
            {
                var user = new ApplicationUser(email);
                // parolayı hashledik
                var hashedPassword = _passwordHasher.HashPassword(password);
                user.SetPasswordHash(hashedPassword);

                _userRepository.Add(user);
                _userRepository.Save();
                var dbUser = _userRepository.Find(user.Id);

                DomainEvent.Raise(new UserCreatedEvent(user));

                if (dbUser == null)
                {
                    result.Errors.Add("Hesap oluşturulamadı");
                }
                else
                {
                    // kullanıcı kaydı oluştu
                    //DomainEvent.Raise(new UserCreatedEvent(user));
                }
            }

            result.IsSucceeded = result.Errors.Count() > 0 ? false:true;

            // userRepository

            return result;
        }


        public ApplicationUserResult UpdateProfile(string firstname, string lastname, string middlename, ApplicationUser user)
        {
            user.SetProfileInfo(firstname, lastname, middlename);
            // repository çağıracağız

            return new ApplicationUserResult
            {
                IsSucceeded = true
            };
        }

        public ApplicationUserResult UpdateProfilePicture(string profilePictureUrl, ApplicationUser user)
        {
            user.SetProfilePicture(profilePictureUrl);
            // repository

            return new ApplicationUserResult
            {
                IsSucceeded = true
            };
        }

     
    }
}
