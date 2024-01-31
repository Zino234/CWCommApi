using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Groups.Response;
using CodeCommApi.Models;
using CodeCommApi.Models.Hubs;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


namespace CodeCommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMapper _mapper;
        private Helpers<ReadGroupDto> x=new Helpers<ReadGroupDto>();
        private readonly IGroupService _service;
        private readonly IUserService _user;
        private readonly IHubContext<CodecommMessageHub> _messageHub;
        private readonly IHubContext<CodeCommGroupHub> _groupHub;

        public GroupController(IMapper mapper,IGroupService service,IUserService user,IHubContext<CodecommMessageHub>messageHub,IHubContext<CodeCommGroupHub> groupHub)
       {
            _mapper = mapper;
            _service = service;
            _user=user;
            _messageHub = messageHub;
            _groupHub = groupHub;
        }



        [HttpPost("[action]/{connectionId}/{UserId}")]
        public async Task<ActionResult<List<UserGroup>>> LoadUser([FromRoute] string connectionId, [FromRoute] Guid UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var data = await _service.LoadsUserGroups(connectionId, UserId);
                var response = x.ConvertToGood("USER GROUPS LOADED SUCCESSFULLY");
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, x.ConvertToBad(ex.Message));
            }
        }



        [HttpGet("GetAllUserGroups/{UserId}")]
        public async Task<ActionResult<DefaultResponse<List<Groups>>>> GetAllUserGroups([FromRoute] Guid UserId)
        {
            var x = new Helpers<List<Groups>>();
            var response = new DefaultResponse<List<Groups>>();
            try
            {
                var result = await _user.GetUserGroups(UserId);
                if (result == null)
                {
                    return StatusCode(404, x.ConvertToBad("USER GROUPS NOT FOUND"));
                }
                response = x.ConvertToGood("USER GROUPS FOUND");
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, x.ConvertToBad(ex.Message));
            }

        }




        [HttpPost]
        [Route("CreateGroup")]

        public async Task<ActionResult<DefaultResponse<ReadGroupDto>>> CreateGroup(CreateGroupDto dto)
        {
                    if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                DefaultResponse<ReadGroupDto> response=new();
     
            try
            {
                Groups group = await _service.CreateGroup(dto);
                if (group == null)
                {
                    return StatusCode(400,x.ConvertToBad("BAD REQUEST"));
                }
               
            
                if (await _service.AddUserToGroup(dto.UserId,group.GroupId))
                {   
                    var groupDto=_mapper.Map<ReadGroupDto>(group);
                   response=x.ConvertToGood("GROUP CREATED SUCCESSFULLY");
                   response.Data=groupDto;
                }
            }
            catch (Exception ex)
            {

                    return StatusCode(500, x.ConvertToBad(ex.Message));
            }
            
                    return Ok(response);

        }

        [HttpGet("GetAllGroups")]
        public async Task<ActionResult<DefaultResponse<List<ReadGroupDto>>>> GetAllGroups()
        {
            var x=new Helpers<List<ReadGroupDto>>();
            var response = new DefaultResponse<List<ReadGroupDto>>();
            // var result = await _context.Groups.ToListAsync();
           try
           {
             var result =await _service.GetAllGroups();
            if (result == null)
            {

                return StatusCode(404,x.ConvertToBad("GROUPS NOT FOUND"));
            }
           var groupDto=result.Select(x=>_mapper.Map<ReadGroupDto>(x));
            response=x.ConvertToGood("GROUPS FETCHED SUCCESSFULLY");
            response.Data = groupDto.ToList();

            return Ok(response);
           }
           catch (Exception ex)
           {
            return StatusCode(500,x.ConvertToBad(ex.Message));
            
           }
        }

        [HttpGet("GetGroupById/{Id}")]
        public async Task<ActionResult<DefaultResponse<ReadGroupDto>>> GetSingleGroup([FromRoute] Guid Id)
        {
            DefaultResponse<ReadGroupDto> response = new ();
           try
           {
             var result = await _service.GetGroupById(Id);
            if (result == null)
            {
              return StatusCode(404,x.ConvertToBad("GROUP NOT FOUND"));
            }
       response=x.ConvertToGood("GROUP FOUND SUCCESSFULLY");
       var groupDto=_mapper.Map<ReadGroupDto>(result);
            response.Data = groupDto;

            return Ok(response);
           }
           catch (Exception ex)
           {
            
            return StatusCode(500,x.ConvertToBad(ex.Message));
           }
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<ActionResult<DefaultResponse<ReadGroupDto>>> UpdateGroup([FromRoute] Guid id, [FromBody] UpdateGroupDto dto)
        {
            var response = new DefaultResponse<ReadGroupDto>();
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
           
            try
            {
                var  result=await _service.UpdateGroup(id,dto);
                if(result==null){
                    return StatusCode(404,x.ConvertToBad("UNABLE TO UPDATE GROUP"));
                }
                var groupDto=_mapper.Map<ReadGroupDto>(result);
                response=x.ConvertToGood("GROUP UPDATED SUCCESSFULLY");
                response.Data=groupDto;
               return Ok(response);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, x.ConvertToBad(ex.Message));
            }
        }

        [HttpDelete("DeleteGroup/{id}")]
        public async Task<ActionResult<DefaultResponse<string>>> DeleteGroup([FromRoute] Guid id)
        {
          try
          {
              bool result = await _service.DeleteGroup(id);
            if (!result)
            {
              return StatusCode(400,x.ConvertToBad("UNABLE TO DELETE GROUP"));
            }
                return StatusCode(204,x.ConvertToGood("GROUP DELETED SUCCESSFULLY"));
          }
          catch (Exception ex)
          {
            return StatusCode(500,x.ConvertToBad(ex.Message));
          }
        }

      

        // [HttpPost("[action]/{userConnectionId}/{groupId}")]
        // public async Task<ActionResult> JoinGroup(string userConnectionId,string groupId){
        //     await _messageHub.Groups.AddToGroupAsync(userConnectionId,groupId);
        //     return Ok();
        // }


        [HttpPost]
        [Route("AddUser/{UserId}/{GroupId}")]
        public async Task<ActionResult<DefaultResponse<ReadGroupDto>>> AddUserToGroup([FromRoute] Guid UserId, [FromRoute] Guid GroupId)
        {
            DefaultResponse<ReadGroupDto> response = new();
            // var user = await _context.Users.AnyAsync(x => x.UserId == UserId);
            // var group = await _context.Groups.AnyAsync(x => x.GroupId == GroupId);
            // if (!user || !group)
            // {
            //     response.Status = false;
            //     response.ResponseCode = "99";
            //     response.ResponseMessage = "Error Getting Details Of ";
            //     response.ResponseMessage += (!user) ? "User" : "Group";
            //     return StatusCode(400, response);
            // }

            try
            {
                bool check=await _service.AddUserToGroup(UserId,GroupId);
                if(!check){
                    
                    return StatusCode(400,x.ConvertToBad("UNABLE TO ADD USER  TO GROUP"));
                }
                
                response=x.ConvertToGood("USER ADDED TO GROUP SUCCESSFULLY");
                var group=await _service.GetGroupById(GroupId);
                var groupDto=_mapper.Map<ReadGroupDto>(group);
                response.Data=groupDto;
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "UNIQUE KEY FAILED: DATA ALREADY REGISTERED");
            }

            


        }
    }
}
