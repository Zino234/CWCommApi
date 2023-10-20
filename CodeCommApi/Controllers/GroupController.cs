using CodeCommApi.Data;
using CodeCommApi.Dto;
using CodeCommApi.Models;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Group = CodeCommApi.Models.Group;

namespace CodeCommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly CodeCommDbContext _context;

        public GroupController(CodeCommDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CreateGroup")]

        public async Task<ActionResult<DefaultResponse<Group>>> CreateGroup(CreateGroupDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new DefaultResponse<Group>();
            try
            {
                Group gop = new Group()
                {
                   GroupName = dto.GroupName,
                   GroupDescription = dto.GroupDescription,
                   GroupLogo = dto.GroupLogo
                };

                await _context.Groups.AddAsync(gop);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.ResponseMessage = "Group Created";
                response.ResponseCode = "00";
                response.Data = gop;
                return Ok(response);
            }
            catch (Exception ex)
            {

                response.Status = false;
                response.ResponseMessage = $"Unable To  Create Group : {ex.Message}";
                response.ResponseCode = "99";
                return StatusCode(500, response);
            }
            
        }
    }
}
