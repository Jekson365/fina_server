using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Dto.groups;
using server.Interfaces;
using server.Models;

namespace server.Controller
{
    [ApiController]
    [Route("api/group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepo;
        public GroupController(IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateGroupDto createGroupDto)
        {
            var result = await _groupRepo.Create(createGroupDto);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var currentGroup = await _groupRepo.Delete(Id);

            return Ok(new { data = currentGroup, deleted = currentGroup });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EditGroupDto group)
        {
            var updatedGroup = await _groupRepo.Update(group);

            return Ok(updatedGroup);
        }
        [HttpGet("default")]
        public async Task<IActionResult> GetAllDefault()
        {
            var result = await _groupRepo.GetAllDefault();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _groupRepo.GetGroups();

            return Ok(result);
        }
    }
}