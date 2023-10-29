namespace RecipesBackend.DAL.Entities.ServiceEntities
{
    public class Measure
    {
        // classify mesure  with name Ex: us, metric
        public string MeasureName { get; set; }
        public double Amount { get; set; }
        public string UnitShort { get; set; }
        public string UnitLong { get; set; }
    }
}
