
using AspNetCoreWebApi6.Models.Dto;
using Microsoft.AspNetCore.Mvc;
 

namespace AspNetCoreWebApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserSevice _userService;

        public UsersController(IUserSevice service)
        {
            _userService = service;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>  GetUsers()
        {

            var usersWithProducts = await _userService.GetUsersWithProductsAsync();

            if (usersWithProducts == null || !usersWithProducts.Any())
            {
                return NotFound();
            }

            return Ok(usersWithProducts);
        }
        /// <summary>
        /// method to get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(string id)
        {

            var User = await _userService.GetAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Ok(User);
        }
        /// <summary>
        /// method to create a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostUser([FromBody]User user)
        {
            if (User == null)
                return BadRequest("Invalid request data.");

            await _userService.CreateAsync(user);


            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        /// <summary>
        /// method to update a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userInput"></param>
        /// <returns></returns>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUser(string id, [FromBody]User userInput)
        {
            if (userInput is null)
            {
                return BadRequest("Invalid request data.");
            }
           
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }
            
            await _userService.UpdateAsync(id, userInput);

            return Ok(user);
        }

        /// <summary>
        /// method to delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var book = await _userService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }

        /// <summary>
        /// method to check if user exists by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        private async Task<bool> UserExists(string email)
        {
            var result = false;
            var list = await _userService.GetAllAsync();
            
            return list.Any(e => string.Equals(e.Email.Trim(), email.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
}
