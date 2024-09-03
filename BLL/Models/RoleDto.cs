using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }

    public class CreateRoleDto
    {
        public string Name { get; set; }
    }

    public class UpdateRoleDto
    {
        public string Name { get; set; }
    }

}
