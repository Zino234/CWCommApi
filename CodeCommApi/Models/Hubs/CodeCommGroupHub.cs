using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Dependencies.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CodeCommApi.Models.Hubs
{

    /// <summary>
    /// THIS IS FOR GROUPS , USER SHOULD BE ABLE TO BE CONNECCTED TO ALL HIS GROUPS FROM HERE 
    /// SEE HOW MANY USERS ARE ONLINE IN THE GROUPP
    /// RECEIVE MESSAGES FROM THE GORUP
    /// SEE WHEN SOMEONE IS TYPING IN THE GRUOP
    /// SEE WHEN A USER LEAVES A GROUP
    /// SEE WHEN SOMEONE NEW JOINS THE GRUOP
    /// SEE WHGEN SOMETHING HAPPENS IN THE GROUP SHA
    /// </summary>


    public class CodeCommGroupHub:CodeCommSharedHub
    {
        private readonly IGroupService _groupService;
        public CodeCommGroupHub(IGroupService groupService)
        {
            _groupService = groupService;
            
        }
    public async Task AskToCheckForNotificationUpdate(Guid GroupId){
        await Clients.Group(GroupId.ToString()).SendAsync("CheckForNotificationUpdate");
    }


    
    }
}