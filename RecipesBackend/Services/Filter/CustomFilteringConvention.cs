using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.MongoDb.Filters;

namespace RecipesBackend.Services.Filter;

public class CustomFilteringConvention : FilterConvention
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.AddDefaults();
        descriptor.Provider(
            new MongoDbFilterProvider(
                x => x
                    .AddFieldHandler<MongoDbStringInvariantContainsHandler>()
                    .AddDefaultMongoDbFieldHandlers()));
    }
}