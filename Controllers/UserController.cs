using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.DynamicData;
using System.Web.Http;
using ReforgedApi.Models;

namespace ReforgedApi.Controllers
{
    public class UserController : ApiController
    {
        private ReforgedContext ctx = new ReforgedContext();

        private List<User> _users;

        public UserController()
        {
            _users = (from i in ctx.Users select i).ToList();
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUser(int id)
        {
            return _users.First(x => x.id == id);
        }

        public List<UserDTO> GetUserByELO()
        {
            var users = (from u in ctx.Users
                join c in ctx.Countries on u.countryId equals c.id
                join f in ctx.Factions on u.favoriteFactionId equals f.id
                orderby s.ELO descending
                select new
                {
                    u.id,
                    u.username,
                    c.code,
                    f.img,
                    
                }).ToList();

            return users;
        }

    }
}
