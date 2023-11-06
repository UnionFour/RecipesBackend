namespace RecipesBackend.DAL.Entities.ServiceEntities
{
    public class Instruction
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageType { get; set; }
    }
}
