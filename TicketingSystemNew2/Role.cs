//using DataAccess;
using Ninject;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TicketingSystemNew2
{
    public class Role : RoleProvider
    {
        [Inject]
        private ILoginService ILoginService;
        //public Role(LoginService LoginService)
        //{
        //    this.ILoginService = LoginService;
        //}
        //public Role(LoginService LoginService) : base()
        //{

        //    this.ILoginService = LoginService;

        //}
        //public Role(string username) :  this(new LoginService())
        //{ 
        //}

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //using (DataContext _Context = new DataContext())
            //{
            var user = DependencyResolver.Current.GetService<ILoginService>();
            //var userRoles =   ILoginService.Rolee(username);
            var userRoles = user.Rolee(username);
                //var userRoles = (from user in _Context.Logins
                //                 join roleMapping in _Context.PermissionUser
                //                 on user.UserID equals roleMapping.UserID
                //                 join role in _Context.Permissions
                //                 on roleMapping.PermissionID equals role.ID
                //                 where user.UserName == username
                //                 select role.Name).ToArray();
                return userRoles;
            //}
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}