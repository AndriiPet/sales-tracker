using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
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
        public string Password { get; set; }
        public int WorkRegionId { get; set; }
        public WorkRegion WorkRegion { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int? ManagerId { get; set; }
        public User Manager { get; set; }
        public ICollection<User> Subordinates { get; set; }
        public ICollection<Trip> Trips { get; set; }
        public ICollection<TradingPoint> TradingPoints { get; set; }
    }
}