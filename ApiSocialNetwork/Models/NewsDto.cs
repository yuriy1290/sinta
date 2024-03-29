namespace ApiSocialNetwork.Models
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int PictureId { get; set; } // Используйте ID изображения
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime SendingTime { get; set; }
        public int IdUser { get; set; }
        public bool LikedByCurrentUser { get; set; } // Указывает, поставил ли текущий пользователь лайк
        public bool DislikedByCurrentUser { get; set; } // Указывает, поставил ли текущий пользователь дизлайк
    }




}
