using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Users.Requests;
using CodeCommApi.Dto.Users.Response;
using CodeCommApi.Models;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Helpers<ReadUserDto> x=new ();
        private IUserService _service;
        private IMapper _mapper;
        public UserController(IMapper mapper, IUserService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<DefaultResponse<ReadUserDto>>> CreateUser(CreateUserDto dto)
        {
            var response = new DefaultResponse<ReadUserDto>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User user = await _service.CreateUser(dto);
                if (user == null)
                {

                    return StatusCode(500, x.ConvertToBad("UNABLE TO CREATE USER"));
                }
                var userDto = _mapper.Map<ReadUserDto>(user);
                response = x.ConvertToGood("USER CREATED SUCCESSFULLY");
                response.Data = userDto;
                return Ok(response);


            }
            catch (Exception ex)
            {

                response = x.ConvertToBad(ex.Message);
                return StatusCode(500, response);
            }
        }





        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<DefaultResponse<List<User>>>> GetAllUsers()
        {
            var x = new Helpers<List<ReadUserDto>>();
            var response = new DefaultResponse<List<ReadUserDto>>();
            try
            {

                var result = await _service.GetUsers();
                if (result == null)
                {
                    return NotFound(x.ConvertToBad("NO USER FOUND"));
                }
                var users = result.Select(x => _mapper.Map<ReadUserDto>(x));
                response = x.ConvertToGood("USERS FOUND SUCCESSFULLY");
                response.Data = users.ToList();
                return Ok(response);

            }
            catch (Exception ex)
            {


                return StatusCode(500, x.ConvertToBad(ex.Message));

            }

        }

[HttpPost("UserLogin")]
public async Task<ActionResult<DefaultResponse<ReadUserDto>>> UserLogin([FromBody]UserLoginDto dto){
        var x=new Helpers<ReadUserDto>();
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        try
        {
            var user=await _service.UserLogin(dto);
            if(user==null){
                return BadRequest(x.ConvertToBad("LOGIN FAILED: BAD CREDENTIALS"));
            }
            var userDto=_mapper.Map<ReadUserDto>(user);
            var response=x.ConvertToGood("LOGIN SUCCESSFULL");
            response.Data=userDto;
            return Ok(response);
        }
        catch (Exception ex)
        {
            
           return StatusCode(500,x.ConvertToBad($"LOGIN FAILED: {ex.Message}"));
        }
}

        [HttpGet]
        [Route("GetUserById/{Id}")]
        public async Task<ActionResult<DefaultResponse<ReadUserDto>>> GetUserById([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<ReadUserDto>();

            try
            {
                var result =await  _service.FindUser(Id);

                if (result == null)
                {
                    response = x.ConvertToBad($"USER WITH ID {Id} : NOT FOUND ");

                    return NotFound(response);

                }
                var userDto = _mapper.Map<ReadUserDto>(result);
                response = x.ConvertToGood("USER FOUND");
                response.Data = userDto;
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad(ex.Message));

            }
        }

        [HttpDelete]
        [Route("DeleteUser/{Id}")]
        public async Task<ActionResult<DefaultResponse<ReadUserDto>>> DeleteUser([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<ReadUserDto>();
            try
            {
                var result = await _service.DeleteUser(Id);
                if (result == false)
                {
                    return StatusCode(500, x.ConvertToBad("UNABLE TO DELETE USER"));
                }

                return Ok(x.ConvertToGood("USER DELETED"));
                // return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, x.ConvertToBad($"SOMETHING WENT WRONG : {ex.Message}"));
            }
        }



        // [HttpGet("GetAllUserGroups/{Id}")]
        // public async Task<ActionResult<DefaultResponse<List<Groups>>>> GetAllUserGroups([FromRoute] Guid Id)
        // {
        //     var x = new Helpers<List<Groups>>();
        //     var response = new DefaultResponse<List<Groups>>();
        //     try
        //     {
        //         var result = await _service.GetUserGroups(Id);
        //         if (result == null)
        //         {
        //             return StatusCode(404, x.ConvertToBad("USER GROUPS NOT FOUND"));
        //         }
        //         response = x.ConvertToGood("USER GROUPS FOUND");
        //         response.Data = result;
        //         return Ok(response);
        //     }
        //     catch (Exception ex)
        //     {

        //         return StatusCode(500, x.ConvertToBad(ex.Message));
        //     }

        // }




        [HttpPut]
        [Route("UpdateUser/{Id}")]
        public async Task<ActionResult<DefaultResponse<ReadUserDto>>> UpdateUser([FromRoute] Guid Id, [FromBody] UpdateUserDto dto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<ReadUserDto>();
            try
            {
                var result=await _service.UpdateUser(Id,dto);
                if (result == null)
                {                  
                      return StatusCode(400,x.ConvertToBad("SOMETHING WENT WRONG,\n COULD NOT COMPLETE THE UPDATE"));
                    }
                var userDto=_mapper.Map<ReadUserDto>(result);
                response=x.ConvertToGood("USER UPDATED SUCCESSFULLY");
                response.Data=userDto;
                return Ok(response);

            }
            catch (DbUpdateConcurrencyException)
            {
                bool Check=_service.FindAny(Id);
                if (!Check)
                {
                    return NotFound(x.ConvertToBad("USER NOT FOUND"));

                }
                else
                {
                    return Conflict();
                }
            }
        }
    }
}
