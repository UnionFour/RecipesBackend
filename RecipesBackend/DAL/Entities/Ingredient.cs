using RecipesBackend.DAL.Entities.ServiceEntities;

namespace RecipesBackend.DAL.Entities
{
    public class Ingredient
    {
        // General Info
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameClean { get; set; }
        public string Original { get; set; }
        public string OriginalName { get; set; }


        // Some WTF fields
        public string Aisle { get; set; }
        public string Photo { get; set; }
        public string Consistency { get; set; }


        public double Amount { get; set; }
        public string AmountUnit { get; set; }


        public List<string> Meta { get; set; }
        public List<Measure> Measures { get; set; }
    }
}
