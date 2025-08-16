using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public interface IAuthProvider
    {
        Task LoginAsync(LoginModel model);
        Task SignupAsync(SignupModel model);
    }
}
