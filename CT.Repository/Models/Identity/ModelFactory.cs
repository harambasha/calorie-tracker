﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using CT.Repository.Infrastructure;
using CT.Repository.Infrastructure.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CT.Repository.Models.Identity
{
    public class ModelFactory
    {

        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _appUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _appUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Level = appUser.Level,
                JoinDate = appUser.JoinDate,
                Roles = _appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _appUserManager.GetClaimsAsync(appUser.Id).Result
            };

        }

        public RoleReturnModel Create(IdentityRole appRole) {

            return new RoleReturnModel
           {
               Url = _urlHelper.Link("GetRoleById", new { id = appRole.Id }),
               Id = appRole.Id,
               Name = appRole.Name
           };

        }

        public static ApplicationUser Update(ApplicationUser oldUser, ApplicationUser newUser)
        {
            oldUser.UserName = newUser.UserName;
            oldUser.FirstName = newUser.FirstName;
            oldUser.LastName = newUser.LastName;
            oldUser.Email = newUser.Email;
            return oldUser;
        }
    }

    public class UserReturnModel
    {

        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }

    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}