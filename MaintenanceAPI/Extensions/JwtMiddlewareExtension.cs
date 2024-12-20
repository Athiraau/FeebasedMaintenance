﻿using Business.Contracts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace MaintenanceAPI.Extensions
{
    public class JwtMiddlewareExtension
    {
        private readonly RequestDelegate _next;
        private readonly IServiceWrapper _serviceWrapper;

        public JwtMiddlewareExtension(RequestDelegate next, IServiceWrapper serviceWrapper)
        {
            _next = next;
            _serviceWrapper = serviceWrapper;

        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(httpContext);
                return;
            }

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _serviceWrapper.JwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach userId to context on successful jwt validation
                httpContext.Items["User"] = userId;
                await _next(httpContext);
            }
            else
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    statusCode = httpContext.Response.StatusCode,
                    message = "Unauthorized"
                }.ToString());
            }



        }
    }
}
