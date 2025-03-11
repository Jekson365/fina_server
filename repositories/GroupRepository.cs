using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Dto.groups;
using server.Interfaces;
using server.Models;

namespace server.repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Group> Create(CreateGroupDto groupDto)
        {
            var groupModel = new Group
            {
                Name = groupDto.Name,
                ParentId = groupDto.ParentId
            };
            await _context.Groups.AddAsync(groupModel);
            await _context.SaveChangesAsync();

            return groupModel;
        }

        public async Task<bool> Delete(int Id)
        {
            var currentModel = await _context.Groups.FindAsync(Id);

            if (currentModel == null)
            {
                return false;
            }

            var refedGroups = _context.Groups.Where(g => g.ParentId == Id);
            foreach (var m in refedGroups)
            {
                _context.Groups.Remove(m);
            }
            _context.Groups.Remove(currentModel);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<DefaultGroupDto>> GetAllDefault()
        {
            var groups = await _context.Groups.Select(e => new DefaultGroupDto
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();

            return groups;
        }

        public async Task<List<Group>> GetGroups()
        {
            var rootGroups = await _context.Groups
                .Where(g => g.ParentId == null)
                .Include(g => g.SubGroups)
                .ToListAsync();

            foreach (var group in rootGroups)
            {
                await LoadSubGroups(group);
            }

            return rootGroups;
        }


        public async Task<Group?> Update(EditGroupDto groupDto)
        {
            var currentGroup = await _context.Groups.FindAsync(groupDto.Id);

            if (currentGroup == null)
            {
                return null;
            }

            currentGroup.Name = groupDto.Name;
            await _context.SaveChangesAsync();

            return currentGroup;
        }

        private async Task LoadSubGroups(Group group)
        {
            group.SubGroups = await _context.Groups
                .Where(g => g.ParentId == group.Id)
                .Include(g => g.SubGroups)
                .ToListAsync();

            foreach (var subGroup in group.SubGroups)
            {
                await LoadSubGroups(subGroup);
            }
        }

    }
}