using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsManager { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; }
        public double LastLatitude { get; set; }
        public double LastLongitude { get; set; }
        public int IPN { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegionDto WorkRegion { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
        public int? ManagerId { get; set; }
        public UserDto Manager { get; set; }
        public ICollection<UserDto> Subordinates { get; set; }
        public ICollection<TripDto> Trips { get; set; }
        public ICollection<TradingPointDto> TradingPoints { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IPN { get; set; }
        public int WorkRegionId { get; set; }
        public int RoleId { get; set; }
        public int? ManagerId { get; set; }
        public bool IsManager { get; set; }
    }

    public class UpdateUserDto 
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ProfilePicture { get; set; }
        public double? LastLatitude { get; set; }
        public double? LastLongitude { get; set; }
        public int? IPN { get; set; }
        public int? WorkRegionId { get; set; }
        public int? RoleId { get; set; }
        public int? ManagerId { get; set; }
        public bool? IsManager { get; set; }
        public bool? IsRegistered { get; set; }
    }
}
