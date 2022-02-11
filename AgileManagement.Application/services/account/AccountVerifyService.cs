using AgileManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    public class AccountVerifyService : IAccountVerifyService
    {
        private readonly IUserRepository _userRepository;

        public AccountVerifyService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Request bizim için userId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool OnProcess(string request)
        {
            var user = _userRepository.Find(request);

            try
            {
                user.SetVerifyEmail();
                _userRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
