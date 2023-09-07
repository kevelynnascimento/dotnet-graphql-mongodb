using GraphQL.Mutations;
using GraphQL.Queries;

namespace GraphQL.Config;

public static class GraphQLBootstraper
{
    public static void BootstrapGraphQL(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddMemoryCache()
            .AddGraphQLServer()
            .AddQueryType()
            .AddMutationType()
            // .AddSubscriptionType() // Turn on that if you would like to use Subscriptions on GraphQL
            .AddTypeExtension<UserQueries>()
            .AddTypeExtension<AddressQueries>()
            .AddTypeExtension<UserMutations>();

        builder.Services.AddErrorFilter<GraphQLErrorFilter>();
    }
}