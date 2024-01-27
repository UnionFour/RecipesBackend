using System.Text.RegularExpressions;
using HotChocolate.Data.Filters;
using HotChocolate.Data.MongoDb;
using HotChocolate.Data.MongoDb.Filters;
using HotChocolate.Language;
using MongoDB.Bson;

namespace RecipesBackend.Services.Filter;

public class MongoDbStringInvariantContainsHandler : MongoDbStringContainsHandler
{
    public MongoDbStringInvariantContainsHandler(InputParser inputParser) : base(inputParser)
    {
    }

    public override MongoDbFilterDefinition HandleOperation(
        MongoDbFilterVisitorContext context, IFilterOperationField field, IValueNode value, object? parsedValue)
    {
        if (parsedValue is not string text)
            throw new InvalidOperationException();

        var doc = new MongoDbFilterOperation("$regex", new BsonRegularExpression($"^{Regex.Escape(text)}", "i"));
        return new MongoDbFilterOperation(context.GetMongoFilterScope().GetPath(), doc);
    }
}