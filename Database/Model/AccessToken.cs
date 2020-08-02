using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Security.Database.Model
{
    public class AccessToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpire { get; set; }
        public int MultipleUseCount { get; set; }

        public ICollection<UserToken> UserTokens { get; set; }

        public AccessToken()
        {
            UserTokens = new Collection<UserToken>();
        }
    }
}
