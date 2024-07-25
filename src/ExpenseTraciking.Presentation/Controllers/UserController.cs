using ExpenseTraciking.Application.Interfaces;
using ExpenseTraciking.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTraciking.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

       
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Yeni kullanıcı ekle
        [HttpPost]
        [Authorize]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // Kullanıcı güncelle
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }

            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userRepository.UpdateUser(user);
            return NoContent();
        }

        // Kullanıcı sil
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }
}
