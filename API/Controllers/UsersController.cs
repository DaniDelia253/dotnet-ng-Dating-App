using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Controllers

{
    // [Authorize]
    //this class is created avery time a user makes a request to the endpoint
    public class UsersController : BaseApiController
    {
        //so this makes use of dependency injection.. 
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        //this gives me a GET request to the endpoint in the attribute above
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        //async and Task here make this an async method
        //Task represents an async operation that can return a value
        //also need the await keyword and async method call below
        //the reason for using ActionResult here is to be able to return HttpResponses (like BadRequest())
        {
            //this request goes, and if it is synchronous, the program will have to wait for it to come back 
            //if it is synchronys code, the server is blocked untill the request comes back
            //with async code, your order is sent and will be given to you when it is ready, but can handle other requests i the meantime
            return await _context.Users.ToListAsync();
            //steps when not using EF:
            //  make a connection
            //  make sure the connection is available
            //  write the SQL query
            //   get back the SQL syntax
            //  map it to the obj we want to return
        }
        //if I want to ensure that only authorized users can access a specific endpoint, I can specify that via an attribute 
        // [Authorize]
        //^this is commented though bc I added this attribute to the whole controller above to protect aLL routes :)
        //so bc of this users will only be able to access this endpoint if they pass an authentication token
        //tell the app HOW to authenticate in the program class (program.cs)
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
            //this request goes, and the program will have to wait for it to come back 
            //if it is synchronys code, the server is blocked untill the request comes back
            //with async code, your order is sent and will be given to you when it is ready, but can handle other requests i the meantime
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<AppUser>> DeleteUser(int id)
        {
            AppUser user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return user;
        }
    }
}

