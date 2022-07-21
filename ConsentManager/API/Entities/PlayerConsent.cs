using LiteDB;

namespace ConsentManager.API.Entities
{
    public class PlayerConsent
    {
        [BsonId]
        public string UserId { get; set; }

        public PlayerConsent()
        {
        }

        public PlayerConsent (string userId)
        {
            UserId = userId;
        }
    }
}
