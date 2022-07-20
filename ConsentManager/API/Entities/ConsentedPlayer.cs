using LiteDB;

namespace ConsentManager.API.Entities
{
    public class ConsentedPlayer
    {
        [BsonId]
        public string UserId { get; set; }

        public ConsentedPlayer()
        {
        }

        public ConsentedPlayer(string userId)
        {
            UserId = userId;
        }
    }
}
