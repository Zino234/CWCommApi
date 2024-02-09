using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Dependencies;
using CodeCommApi.Dependencies.Interfaces;
using CodeCommApi.Dto.Post.Request;
using CodeCommApi.Dto.Post.Response;
using CodeCommApi.Response;
using Microsoft.AspNetCore.Mvc;

namespace CodeCommApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private Helpers<ReadPostDto> x = new();
        private readonly IPostService _service;
        private readonly IUserService _user;

        public PostController(IMapper mapper, IPostService service, IUserService user)
        {
            _mapper = mapper;
            _service = service;
            _user = user;
        }



    [HttpPost("CreatePost/{UserId}")]
    public async Task<ActionResult<DefaultResponse<ReadPostDto>>> CreatePost([FromRoute]Guid UserId,[FromBody]CreatePostDto dto){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   

        var PostRequest=_mapper.Map<Models.Post>(dto);
        PostRequest.UserId=UserId;
        var post=await _service.CreatePost(PostRequest);
        if(post==null){
            return BadRequest(x.ConvertToBad("UNABLE TO CREATE POST"));
        }
        var postDto=_mapper.Map<ReadPostDto>(post);
        var response=x.ConvertToGood("POST CREATED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(201,response);

    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
    
    }



    [HttpGet("GetAll")]
    public async Task<ActionResult<DefaultResponse<IEnumerable<ReadPostDto>>>> GetAllPost(){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   
        var x=new Helpers<List<ReadPostDto>>();

        var posts=await _service.GetAllPosts();
        if(posts==null || posts.Count()==0){
            return BadRequest(x.ConvertToBad("UNABLE TO GET POSTS || NO POOST FOUND"));
        }
        var postDto=_mapper.Map<List<ReadPostDto>>(posts);
        var response=x.ConvertToGood("POST FETCHED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(201,response);

    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
     


    }

    [HttpGet("GetUserPosts/{UserId}")]
    public async Task<ActionResult<DefaultResponse<IEnumerable<ReadPostDto>>>> GetUserPosts([FromRoute]Guid UserId){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   
        var x=new Helpers<List<ReadPostDto>>();

        var posts=await _service.GetUserPosts(UserId);
        if(posts==null || posts.Count()==0){
            return BadRequest(x.ConvertToBad("UNABLE TO GET POSTS || NO POOST FOUND"));
        }
        var postDto=_mapper.Map<List<ReadPostDto>>(posts);
        var response=x.ConvertToGood("POST FETCHED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(200,response);

    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
     


    }



    [HttpGet("GetUserPosts/{PostId}")]
    public async Task<ActionResult<DefaultResponse<ReadPostDto>>> GetPostById([FromRoute]Guid PostId){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   

        var posts=await _service.GetPostById(PostId);
        if(posts==null){
            return BadRequest(x.ConvertToBad("UNABLE TO GET POSTS || INVALID POST ID"));
        }
        var postDto=_mapper.Map<ReadPostDto>(posts);
        var response=x.ConvertToGood("POST FETCHED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(200,response);

    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
     


    }
   
   
    [HttpDelete("DeletePost/{PostId}")]
    public async Task<ActionResult<DefaultResponse<ReadPostDto>>> DeletePost([FromRoute]Guid PostId){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   

        var posts=await _service.DeletePost(PostId);
        if(posts==null){
            return BadRequest(x.ConvertToBad("UNABLE TO DELETE POSTS || INVALID POST ID"));
        }
        var postDto=_mapper.Map<ReadPostDto>(posts);
        var response=x.ConvertToGood("POST DELETED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(200,response);
    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
     


    }



    [HttpPut("EditPost/{PostId}")]
    public async Task<ActionResult<DefaultResponse<ReadPostDto>>> EditPost([FromRoute]Guid PostId,[FromBody]UpdatePostDto dto){
    if(!ModelState.IsValid){
        return BadRequest(ModelState);       
    }
    try
    {   

        var posts=await _service.EditPost(PostId,_mapper.Map<Models.Post>(dto));
        if(posts==null){
            return BadRequest(x.ConvertToBad("UNABLE TO UPDATE POSTS || INVALID POST ID"));
        }
        var postDto=_mapper.Map<ReadPostDto>(posts);
        var response=x.ConvertToGood("POST UPDATED SUCCESSFULLY");
        response.Data=postDto;
        return StatusCode(200,response);
    }
    catch (System.Exception ex)
    {
        return StatusCode(500,x.ConvertToBad($"{ex.Message}"));
    }
     


    }

















    }
}
