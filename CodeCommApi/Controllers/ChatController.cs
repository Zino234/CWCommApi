using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Data;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.interfaces;
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
        private Helpers<ReadChatDto> x;
        


        public ChatController(IMapper mapper,IChatService service)
        {
            _mapper = mapper;
            _service = service;
            
        }



    [HttpPost("/CreateChat")]
    public async Task<ActionResult<DefaultResponse<ReadChatDto>>> CreateChat(CreateChatDto dto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        DefaultResponse<ReadChatDto> response;
        try
        {
            var chat=await _service.CreateChat(dto);
            if(chat==null){
                response=x.ConvertToBad("Unable To Create Chat");
                return StatusCode(400,response);
            }
            response=x.ConvertToGood("Chat Created Successfully");
            response.Data=_mapper.Map<ReadChatDto>(chat);
            return Ok(response);
        }
        catch (System.Exception ex)
        {
            return StatusCode(500,x.ConvertToBad(ex.Message));
        }

    }




    // [HttpGet("Get")]













    }
}