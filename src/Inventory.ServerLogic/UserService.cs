using Inventory.Models;
using Inventory.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic
{
    public class UserService(SignInManager<User> manager)
    {
        private readonly SignInManager<User> _manager = manager;

        public async Task SignupAsync(SignupModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var res = await _manager.UserManager.CreateAsync(user, model.Password ?? string.Empty);

            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description).ToImmutableArray();
            }
        }

    }
}
