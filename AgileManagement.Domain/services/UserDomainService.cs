using AgileManagement.Core;
using AgileManagement.Core.domain;
using AgileManagement.Domain;
using AgileManagement.Domain.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Domain
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _applicationUserRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserDomainService(IUserRepository applicationUserRepository, IPasswordHasher passwordHasher)
        {
            _applicationUserRepository = applicationUserRepository;
            _passwordHasher = passwordHasher;
        }

        public ApplicationUserResult CreateUser(string email, string password)
        {            
            var result = new ApplicationUserResult() { IsSucceeded = true };

            // email unique mi
            var emailExists = _applicationUserRepository.GetQuery().FirstOrDefault(x => x.Email == email) == null ? false:true;

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

                _applicationUserRepository.Add(user);
                var dbUser = _applicationUserRepository.Find(user.Id);

            
                if (dbUser == null)
                {
                    result.Errors.Add("Hesap oluşturulamadı");
                }
                else
                {
                    // kullanıcı kaydı oluştu
                    DomainEvent.Raise(new UserCreatedEvent(user));
                }
            }

            result.IsSucceeded = result.Errors.Count() > 0 ? false:true;

            // userRepository

            return result;
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            throw new NotImplementedException();
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
