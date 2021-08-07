using System;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Role { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}