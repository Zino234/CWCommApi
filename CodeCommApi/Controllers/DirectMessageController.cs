using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.DirectMessages.Requests;
using CodeCommApi.Dto.DirectMessages.Response;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodeCommApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectMessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDirectMessageService  _service;

        public DirectMessageController(IMapper mapper,IDirectMessageService service)
        {
            _service=service;
            _mapper = mapper;
        }


        [HttpPost("CreateDirectMessage/{ChatId}/{SenderId}/{ReceiverId}")]
        public async Task<ActionResult<DefaultResponse<ReadDirectMessageDto>>> CreateDirectMessage([FromRoute]Guid ChatId,[FromRoute]Guid SenderId,[FromRoute]Guid ReceiverId,[FromBody]CreateDirectMessageDto dto){
            
            var x=new Helpers<ReadDirectMessageDto>();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var message=await _service.CreateDirectMessage(ChatId,SenderId,ReceiverId,dto);
                if(message==null){
                    return BadRequest(x.ConvertToBad("UNABLE TO CREATE MESSAGE"));
                }
                var response=x.ConvertToGood("MESSAGE CREATED SUCCESSFULLY");
                var messageDto=_mapper.Map<ReadDirectMessageDto>(message);
                response.Data=messageDto;
                return Ok(response);

            }
            catch (Exception ex)
            {
                
                return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
            }
        }
    
    [HttpGet("GetChatMessages/{ChatId}")]
    public async Task<ActionResult<DefaultResponse<List<ReadDirectMessageDto>>>> GetChatMessages([FromRoute]Guid ChatId){
        var x=new Helpers<List<ReadDirectMessageDto>>();
        try
        {
            var messages=await _service.GetChatMessages(ChatId);
            if(messages==null){
                return NotFound(x.ConvertToBad("MESSAGES NOT FOUND"));
            }
            var response=x.ConvertToGood("MESSAGES FOUND SUCCESSFULLY");
            var messageList=messages.Select(x=>_mapper.Map<ReadDirectMessageDto>(x)).ToList();
            response.Data=messageList;
            return Ok(response);
        }
        catch (Exception ex)
        {
            
            return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
        }
    }
    
    

[HttpGet("GetMessageById/{MessageId}")]
public async Task<ActionResult<DefaultResponse<ReadDirectMessageDto>>> GetSingleMessage([FromRoute]Guid MessageId)
{
    var x=new Helpers<ReadDirectMessageDto>();
    try
    {
        var message=await _service.GetSingleMessage(MessageId);
        if(message==null){
            return NotFound(x.ConvertToBad("NO MESSAGE FOUND"));

        }
        var response=x.ConvertToGood("MESSAGE FOUND SUCCESSFULLY");
        var messageDto=_mapper.Map<ReadDirectMessageDto>(message);
        response.Data=messageDto;
        return Ok(response);
    }
    catch (Exception ex)
    {
        
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }


}


[HttpPut("UpdateMessage/{MessageId}")]
public async Task<ActionResult<DefaultResponse<ReadDirectMessageDto>>> UpdateMessage([FromRoute]Guid MessageId,[FromBody]UpdateDirectMessageDto dto){
    var x=new Helpers<ReadDirectMessageDto>();
    try
    {
        var data=await _service.UpdateMessage(MessageId,dto);
        if(data==null){
            return BadRequest(x.ConvertToBad("UNABLE TO UPDATE MESSAGE"));
        }
        var response=x.ConvertToGood("MESSAGE UPDATED SUCCESSFULLY");
        var messageDto=_mapper.Map<ReadDirectMessageDto>(data);
        response.Data=messageDto;
        return Ok(response);
    }
    catch (Exception ex)
    {
       return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
}

[HttpDelete("DeleteMessage/{MessageId}")]
public async Task<ActionResult<DefaultResponse<bool>>> DeleteMessage([FromRoute]Guid MessageId){
     var x=new Helpers<bool>();
    try
    {
        bool check=await _service.DeleteMessage(MessageId);
        if(!check){
            return BadRequest(x.ConvertToBad("UNABLE TO DELETE MESSAGE"));
        }
        return NoContent();
        
    }
    catch (Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
}
   
    
    }
}