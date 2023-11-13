using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.interfaces;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.Chats.Requests;
using CodeCommApi.Dto.Chats.Response;
using CodeCommApi.Models;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodeCommApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IChatService _service;
        // private Helpers<ReadChatDto> x;


        private readonly IUserService _user;

        public ChatController(IMapper mapper, IChatService service,IUserService user)
        {
            _user = user;
            _mapper = mapper;
            _service = service;

        }



        [HttpPost("/CreateChat")]
        public async Task<ActionResult<DefaultResponse<ReadChatDto>>> CreateChat(CreateChatDto dto)
        {
            var x = new Helpers<ReadChatDto>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            DefaultResponse<ReadChatDto> response;
            try
            {
                var chat = await _service.CreateChat(dto);
                if (chat == null)
                {
                    response = x.ConvertToBad("UNABLE TO CREATE CHAT");
                    return StatusCode(400, response);
                }
                response = x.ConvertToGood("CHAT CREATED  SUCCESS");
                response.Data = _mapper.Map<ReadChatDto>(chat);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, x.ConvertToBad(ex.Message));
            }

        }


        [HttpGet("GetChatByUsers/{User1}/{User2}")]
        public async Task<ActionResult<DefaultResponse<Chat>>> GetChatByUsers([FromRoute] Guid User1, [FromRoute] Guid User2)
        {
            // var x = new Helpers<ReadChatDto>();
            var x = new Helpers<Chat>();
            Chat chat = await _service.GetChatByUsers(User1,User2);
            if (chat == null)
            {

                return NotFound(x.ConvertToBad("CHAT NOT FOUND"));
            }
            // chat.User1=await _user.FindUser(User1);
            // chat.User2=await _user.FindUser(User2);

            var response = x.ConvertToGood("CHAT FOUND SUCCESSFULLY");
            // var chatDto = _mapper.Map<ReadChatDto>(chat);
            // response.Data = chatDto;
            response.Data = chat;
            return Ok(response);

        }

        [HttpGet("GetChatById/{ChatId}")]
        public async Task<ActionResult<DefaultResponse<ReadChatDto>>> GetChatById([FromRoute] Guid ChatId)
        {
            var x = new Helpers<ReadChatDto>();
try
{
                var chat = await _service.GetChatById(ChatId);
            if (chat == null)
            {         return NotFound(x.ConvertToBad("CHAT NOT FOUND"));
            }
            var response = x.ConvertToGood("CHAT FOUND SUCCESSFULLY");
            var chatDto = _mapper.Map<ReadChatDto>(chat);
            response.Data = chatDto;
            return Ok(response);
}
catch (Exception ex)
{
    
    return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
}
        }
        [HttpGet("GetUserChats/{UserId}")]
        public async Task<ActionResult<DefaultResponse<List<ReadChatDto>>>> GetChatByUser([FromRoute] Guid UserId)
        {
            var x = new Helpers<List<ReadChatDto>>();
            var chat = await _service.GetUserChats(UserId);
            if (chat == null)
            {
                return NotFound(x.ConvertToBad("CHAT NOT FOUND"));
            }
            var response = x.ConvertToGood("CHAT FOUND SUCCESSFULLY");
            var chatDto = chat.Select(x=>_mapper.Map<ReadChatDto>(x)).ToList();
            response.Data = chatDto;
            return Ok(response);
        }


        [HttpDelete("DeleteChat/{ChatId}")]
        public async Task<ActionResult<DefaultResponse<ReadChatDto>>> DeleteChat([FromRoute]Guid ChatId){
            var x=new Helpers<ReadChatDto>();
            DefaultResponse<ReadChatDto> response;
            try
            {
                bool check=await _service.DeleteChat(ChatId);
                if(!check){
                     response=x.ConvertToGood("ERROR DELETING CHAT");
                return StatusCode(400,response);

                }
                 response=x.ConvertToGood("CHAT DELETED SUCCESSFULLY");
                return NoContent();
            }
            catch ( Exception ex)
            {
                
                return x.ConvertToBad($"{ex.Message}");
            }

        }













    }
}