using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UMApi.Dtos;
using UMApi.Helpers;
using UMApi.Models;
using UMApi.Services;

namespace UMApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public RoleController(IRoleService roleService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _roleService = roleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        [HttpGet("{id}", Name = "GetRoleById")]

        public ActionResult<CreateRoleDto> GetById(int? id)
        {
            Role role = _roleService.GetById(id);
            if (role != null)
            {
                return Ok(_mapper.Map<CreateRoleDto>(role));
            }
            return NotFound();
        }
        //POST 
        [AllowAnonymous]
        [HttpPost("Create")]
        public ActionResult CreateRole([FromBody] CreateRoleDto roleDto)
        {
            var userModel = _mapper.Map<Role>(roleDto);


            try
            {
                // create user
               Role role = _roleService.Create(userModel);
               _roleService.SaveChanges();
               
                return CreatedAtAction(nameof(GetById), new { id = role.Id }, roleDto);

            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            var roleList = _roleService.GetAll();
            return Ok(_mapper.Map<IEnumerable<CreateRoleDto>>(roleList));
        }

        //PUT {id}
        [HttpPut("{id}")]
       // [AllowAnonymous]
        public ActionResult Update(int id, CreateRoleDto roleDtoUpt)
        {
            var roleModel = _roleService.GetById(id);
            if (roleModel == null)
            {
                return NotFound();
            }
            _mapper.Map(roleDtoUpt, roleModel);
            _roleService.Update(roleModel);
            _roleService.SaveChanges();
            return NoContent();

        }
    }
}
