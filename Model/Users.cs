using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APPROG7311.Model
{
    public class Users
    {
        [Key]

        [Required]
        [DataType(DataType.Text)]
        public int Id { get; set; }
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public ICollection<Users> User { get; set; }
    }
}
