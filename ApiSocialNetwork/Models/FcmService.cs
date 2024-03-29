namespace ApiSocialNetwork.Models
{
    public class FcmService
    {
        private readonly string FcmServerKey = "Push/socialnetwork-40154-firebase-adminsdk-tpfbq-1c1b87e3c6.json";
        private readonly string FcmEndpoint = "https://fcm.googleapis.com/fcm/send";

        public async Task SendNotification(string deviceToken, string title, string body)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={FcmServerKey}");
                var payload = new
                {
                    to = deviceToken,
                    notification = new
                    {
                        title = title,
                        body = body
                    }
                };

                var response = await client.PostAsJsonAsync(FcmEndpoint, payload);
                // Обработка ответа, если необходимо
            }
        }
    }
}
