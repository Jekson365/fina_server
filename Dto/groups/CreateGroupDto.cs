using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Dto.groups
{
    public class CreateGroupDto
    {
        public string? Name { get; set; }
        public int? ParentId { get; set; }
    }
}