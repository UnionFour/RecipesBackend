namespace RecipesBackend.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string NickName { get; set; }
        public string AboutUser { get; set; }
        public string Photo { get; set; }

        public string Login { get; set; }
        public string HashPassword { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime UpdateTime { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
