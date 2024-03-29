namespace ApiSocialNetwork.Models
{
    public class FcmToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }

        public User User { get; set; }
    }
}
