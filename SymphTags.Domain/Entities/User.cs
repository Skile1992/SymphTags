using System.Collections.Generic;

namespace SymphTags.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool EmailVerified { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}
