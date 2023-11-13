using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto;
using CodeCommApi.Dto.GroupMessage.Request;
using CodeCommApi.Dto.GroupMessage.Response;
using CodeCommApi.Dto.Groups.Response;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodeCommApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupMessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupMessageService _service;
        private Helpers<ReadGroupMessageDto> x = new();

        public GroupMessageController(IMapper mapper, IGroupMessageService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("SendGroupMessage/{UserId}")]
        public async Task<ActionResult<DefaultResponse<ReadGroupMessageDto>>> CreateGroupMessage(
            [FromRoute] Guid UserId,
            [FromBody] CreateGroupMessageDto dto
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var message = await _service.CreateGroupMessage(UserId, dto);
                if (message == null)
                {
                    return StatusCode(400, x.ConvertToBad("INVALID PARAMETER"));
                }
                var response = x.ConvertToGood("MESSAGE SENT SUCCESSFULLY");
                var messageDto = _mapper.Map<ReadGroupMessageDto>(message);
                response.Data = messageDto;
                return Ok(response);
            }
            catch (Exception ex)
            {
                StatusCode(500, x.ConvertToBad($"{ex.Message}"));
            }
            return Ok("SOMETHING WENT WRONG: MESSAGE COULD NOT BE SENT");
        }
   
   [HttpGet("GetGroupMessages/{GroupId}")]
   public async Task<ActionResult<DefaultResponse<List<ReadGroupMessageDto>>>> GetGroupMessages([FromRoute]Guid GroupId){

    try
    {
        var x=new Helpers<List<ReadGroupMessageDto>>();
        var groups=await _service.GetAllGroupMessages(GroupId);
        if(groups==null){
            return NotFound(x.ConvertToGood("GROUP MESSAGES NOT FOUND"));
        }
        var groupDto=groups.Select(x=>_mapper.Map<ReadGroupMessageDto>(x)).ToList();
        var response=x.ConvertToGood("MESSAGES  FETCHED SUCCESSFULLY");
        response.Data=groupDto;
        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }

   }
   
   [HttpGet("GetSingleMessage/{MessageId}")]
   public async Task<ActionResult<DefaultResponse<ReadGroupMessageDto>>> GetSingleMessages([FromRoute]Guid MessageId){

    try
    {
        var message=await _service.GetMessageById(MessageId);
        if(message==null){
            return NotFound(x.ConvertToGood("GROUP MESSAGE NOT FOUND"));
        }
        var messageDto=_mapper.Map<ReadGroupMessageDto>(message);
        var response=x.ConvertToGood("MESSAGES  FETCHED SUCCESSFULLY");
        response.Data=messageDto;
        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }

   }
   
   
   [HttpPut("UpdateMessage/{MessageId}")]
   public async Task<ActionResult<DefaultResponse<ReadGroupMessageDto>>> UpdateMessages([FromRoute]Guid MessageId,UpdateGroupMessageDto dto){

    try
    {
        var message=await _service.UpdateMessage(MessageId,dto);
        if(message==null){
            return NotFound(x.ConvertToGood("ERROR  UPDATING MESSAGE"));
        }
        var messageDto=_mapper.Map<ReadGroupMessageDto>(message);
        var response=x.ConvertToGood("MESSAGES  UPDATED SUCCESSFULLY");
        response.Data=messageDto;
        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }

   }
   
   
   
   
   
   
   
   
   
   
   
   
   
    }
}
