using System.Security.Cryptography;
using System.Text;

namespace SymphTags.Persistance
{
    public class ContextInitializer
    {
        //used for populating test data
        public static void Initialize(Context context)
        {
            SeedUsers(context); 
            SeedLinks(context);
            SeedTags(context);
            SeedLinkTags(context);
        }

        private static void SeedLinks(Context context)
        {
            context.Link.Add(new Domain.Entities.Link
            {
                Id = 1,
                Url = "https://en.wikipedia.org/wiki/Computer_programming",
                UserIdCreated = 1
            });

            context.Link.Add(new Domain.Entities.Link
            {
                Id = 2,
                Url = "https://www.reddit.com/",
                UserIdCreated = 1
            });

            context.Link.Add(new Domain.Entities.Link
            {
                Id = 3,
                Url = "https://9gag.com/",
                UserIdCreated = 2
            });

            context.SaveChanges();
        }

        private static void SeedTags(Context context)
        {
            context.Tag.Add(new Domain.Entities.Tag
            {
                Id = 1,
                Name = "fun"
            });

            context.Tag.Add(new Domain.Entities.Tag
            {
                Id = 2,
                Name = "learning"
            });

            context.Tag.Add(new Domain.Entities.Tag
            {
                Id = 3,
                Name = "website"

            });

            context.SaveChanges();
        }

        public static void SeedLinkTags(Context context)
        {
            context.LinkTags.Add(new Domain.Entities.LinkTags
            {
                LinkId = 1,
                TagId = 1
            });
            context.LinkTags.Add(new Domain.Entities.LinkTags
            {
                LinkId = 2,
                TagId = 1
            });
            context.LinkTags.Add(new Domain.Entities.LinkTags
            {
                LinkId = 3,
                TagId = 1
            });
            context.LinkTags.Add(new Domain.Entities.LinkTags
            {
                LinkId = 1,
                TagId = 2
            });

            context.SaveChanges();
        }

        private static void SeedUsers(Context context)
        {
            //must encript their passwords with random salt
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes("mySalt"));

            context.User.Add(new Domain.Entities.User
            {
                Id = 1,
                DisplayName = "Symphony",
                Email = "symphony@gmail.com",
                EmailVerified = true,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("symphony")),
                Salt = Encoding.UTF8.GetBytes("mySalt")
            });

            context.User.Add(new Domain.Entities.User
            {
                Id = 2,
                DisplayName = "test",
                Email = "test@gmail.com",
                EmailVerified = false,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("test")),
                Salt = Encoding.UTF8.GetBytes("mySalt")
            });

            context.SaveChanges();
        }
    }
}
