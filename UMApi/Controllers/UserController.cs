using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UMApi.Dtos;
using UMApi.Helpers;
using UMApi.Models;
using UMApi.Services;

namespace UMApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UserController(IUserService userService, 
            IMapper mapper, 
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Authenticate model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }

        //GET
        [Authorize(Roles = "1")]
        [HttpGet("{id}", Name = "GetById")]

        public ActionResult<ReadUserDto> GetById (int id)
        {
            User user = _userService.GetById(id);
            if (user != null)
            {
                return Ok(_mapper.Map<ReadUserDto>(user));
            }
            return NotFound();
        }

        //GET ALL
       
        [HttpGet]
        public ActionResult<IEnumerable<ReadUserDto>> GetAll()
        {
            var usersList =  _userService.GetAll();
            return Ok(  _mapper.Map<IEnumerable<ReadUserDto>>(usersList));
        }

        //POST 
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public ActionResult CreateUser(CreateUserDto cmd)
        {
            var userModel = _mapper.Map<User>(cmd);
          

            try
            {
                // create user
                _userService.Create(userModel, cmd.Password);
                _userService.SaveChanges();
                ReadUserDto readUserDto = _mapper.Map<ReadUserDto>(userModel);
                return CreatedAtAction(nameof(GetById), new { id = readUserDto.Id }, readUserDto);
               
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateUserDto userUPt)
        {
            // map model to entity and set id
            var user = _mapper.Map<User>(userUPt);
            user.Id = id;
            try
            {
                // update user 
                _userService.Update(user);
                _userService.SaveChanges();
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpDelete("{id}")]
        
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            _userService.SaveChanges();
            return Ok("Deleted");
        }

 
    }
}
