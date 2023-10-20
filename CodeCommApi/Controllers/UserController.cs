using CodeCommApi.Data;
using CodeCommApi.Dto;
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
        private readonly CodeCommDbContext _context;
        public UserController(CodeCommDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<DefaultResponse<User>>> CreateUser(CreateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = new DefaultResponse<User>();
            try
            {
                User Use = new User()
                {
                    Username = dto.Username,
                    UserEmail = dto.Email,
                    UserPhone = dto.Phone,
                    UserPassword = dto.ConfirmPassword,
                    UserProfilePicUrl = dto.ProfilePicUrl
                };

                await _context.Users.AddAsync(Use);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.ResponseMessage = "User Created";
                response.ResponseCode = "00";
                response.Data = Use;
                return Ok(response);


            }
            catch (Exception ex)
            {

                response.Status = false;
                response.ResponseMessage = $"Unable To  Create User : {ex.Message}";
                response.ResponseCode = "99";
                return StatusCode(500, response);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<DefaultResponse<List<User>>>> GetAllUsers()
        {
            var response = new DefaultResponse<List<User>>();
            try
            {
                var result = await _context.Users.ToListAsync();

                if(result.Count > 0)
                {
                    response.Status = true;
                    response.ResponseMessage = "Succesful";
                    response.ResponseCode = "00";
                    response.Data = result;
                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $" Something Went Wrong : {ex.Message}";
                response.ResponseCode = "99";
                
                return StatusCode(500, response);

            }

            return Ok();
        }


        [HttpGet]
        [Route("GetUserById/{Id}")]
        public async Task<ActionResult<DefaultResponse<User>>> GetUserById([FromRoute] Guid Id)
        {
            var response = new DefaultResponse<User>();

            try
            {
                var result = await _context.Users.FindAsync(Id);
                if(result == null)
                {
                    response.Status = false;
                    response.ResponseMessage = $"user not found";
                    response.ResponseCode = "99";
                   
                    return NotFound(response);

                }
                response.Status = true;
                response.ResponseMessage = "User found";
                response.ResponseCode = "00";
                response.Data = result;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ResponseMessage = $"Unable To  Get User : {ex.Message}";
                response.ResponseCode = "99";
                return StatusCode(500, response);

            }
        }

        [HttpDelete]
        [Route("DeleteUser/{Id}")]
        public async Task<ActionResult<DefaultResponse<User>>> DeleteUser([FromRoute] Guid  Id)
        {
            var response = new DefaultResponse<User>();
            try
            {
                var result = await _context.Users.FindAsync(Id);
                if(result == null)
                {
                    response.Status = false;
                    response.ResponseMessage = $"unable to delete user";
                    response.ResponseCode = "99";

                    //return NotFound(response);
                    return NoContent();
                }

                _context.Users.Remove(result);
                _context.Entry(result).State = EntityState.Deleted;
                await _context.SaveChangesAsync();  
                response.Status = true;
                response.ResponseMessage = "User Deleted";
                response.ResponseCode = "00";
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Status = false;
                response.ResponseMessage = $"Something went wrong : {ex.Message}";
                response.ResponseCode = "99";
                return StatusCode(500, response);
            }
        }


        [HttpPut]
        [Route("UpdateUser/{Id}")]
        public async Task<ActionResult<DefaultResponse<User>>> UpdateUser([FromRoute] Guid Id, [FromBody] UpdateUserDto dto)
        {
            var response = new DefaultResponse<User>();
            try
            {
                var result = await _context.Users.FindAsync(Id);
                if (result == null)
                {
                    response.Status = false;
                    response.ResponseMessage = $"User Not Found";
                    response.ResponseCode = "99";
                    return NotFound();

                }

                result.Username = dto.Username;
                result.UserPhone = dto.Phone;
                result.UserEmail = dto.Email;
                result.UserProfilePicUrl = dto.ProfilePicUrl;
                result.UserPassword = dto.ConfirmPassword;

                _context.Entry(result).State= EntityState.Modified;
                response.Status = true;
                response.ResponseMessage = "Updated Successfully";
                response.ResponseCode = "00";
                response.Data = result;
                await _context.SaveChangesAsync();
                return Ok(response);

            }
            catch (DbUpdateConcurrencyException)
            {
                bool Check = _context.Users.Any(x => x.UserId == Id);
                if (!Check)
                {
                    response.Status = false;
                    response.ResponseMessage = $"User Not Found";
                    response.ResponseCode = "99";
                    return NotFound();

                }
                else
                {
                    return Conflict();
                }
            }
        }
    }
}
