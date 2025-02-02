﻿using Inventory.WebAPI.Services;

namespace Inventory.WebAPI.Middlewares
{
    public class UserMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public Task InvokeAsync(HttpContext context, UserInfo user)
        {
            if (context.User.Identity?.IsAuthenticated == true)
                user.Populate(context);
            return _next(context);
        }

    }
}
