using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using API.Context;
using API.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    
    public class UsersController:BaseController
    {
        private readonly DataContext DbContext;
        public UsersController(DataContext DbContext)
        {
            this.DbContext=DbContext;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> getUsers()
        {
            return DbContext.Users.ToList();
        }
        
        [Authorize]
        [HttpGet("id")]
        public ActionResult<AppUser> getUsers(int id)
        {
            return DbContext.Users.Find(id);
        }
    }
}