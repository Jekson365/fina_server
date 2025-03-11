using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dto.groups;
using server.Models;

namespace server.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group> Create(CreateGroupDto groupDto);
        public Task<Group> Update(EditGroupDto groupDto);
        public Task<bool> Delete(int Id);
        public Task<List<DefaultGroupDto>> GetAllDefault();
        public Task<List<Group>> GetGroups();
   
    }


}