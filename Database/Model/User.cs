using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Security.Database.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<UserToken> UserTokens { get; set; }

        public User()
        {
            UserTokens = new Collection<UserToken>();
        }
    }
}
