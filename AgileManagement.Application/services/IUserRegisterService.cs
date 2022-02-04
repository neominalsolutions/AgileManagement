using AgileManagement.Application;
using AgileManagement.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileManagement.Application
{
    /// <summary>
    /// Bu servis ile uygulama gelen user create istediğini koordine ediceğiz.
    /// </summary>
    public interface IUserRegisterService: IApplicationService<UserRegisterRequestDto,UserRegisterResponseDto>
    {

    }
}
