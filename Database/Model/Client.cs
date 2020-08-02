using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Security.Database.Model
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public ICollection<UserToken> UserTokens { get; set; }

        public Client()
        {
            UserTokens = new Collection<UserToken>();
        }
    }
}
