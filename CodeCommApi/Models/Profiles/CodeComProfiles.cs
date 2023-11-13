using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeCommApi.Dto;
using CodeCommApi.Dto.Chats.Requests;
using CodeCommApi.Dto.Chats.Response;
using CodeCommApi.Dto.DirectMessages.Requests;
using CodeCommApi.Dto.DirectMessages.Response;
using CodeCommApi.Dto.GroupMessage.Request;
using CodeCommApi.Dto.GroupMessage.Response;
using CodeCommApi.Dto.Groups.Response;
using CodeCommApi.Dto.Users.Response;

namespace CodeCommApi.Models.Profiles
{
    public class CodeComProfiles : Profile
    {
        public CodeComProfiles()
        {
        CreateMap<CreateChatDto,Chat>();
        CreateMap<Chat,ReadChatDto>();
        CreateMap<CreateUserDto,User>();
        CreateMap<User,ReadUserDto>();
        CreateMap<CreateGroupDto,Groups>();
        CreateMap<UpdateGroupDto,Groups>();
        CreateMap<Groups,ReadGroupDto>();
        CreateMap<CreateDirectMessageDto,DirectMessage>();
        CreateMap<UpdateDirectMessageDto,DirectMessage>();
        CreateMap<DirectMessage,ReadDirectMessageDto>();
        CreateMap<CreateGroupMessageDto,GroupMessage>();
        CreateMap<UpdateGroupMessageDto,GroupMessage>();
        CreateMap<GroupMessage,ReadGroupMessageDto>();

    
        }
    }
}