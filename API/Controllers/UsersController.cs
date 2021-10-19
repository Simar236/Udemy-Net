using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using API.Context;
using API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController:ControllerBase
    {
        private readonly DataContext DbContext;
        public UsersController(DataContext DbContext)
        {
            this.DbContext=DbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> getUsers()
        {
            return DbContext.Users.ToList();
        }
        [HttpGet("id")]
        public ActionResult<AppUser> getUsers(int id)
        {
            return DbContext.Users.Find(id);
        }
    }
}