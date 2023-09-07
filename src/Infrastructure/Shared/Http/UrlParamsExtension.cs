using System.Text;

namespace Infrastructure.Shared.Http;

public static class UrlParamsExtension
{
    public static string ToUrlParams(this Dictionary<string, string> queryStringParams)
    {
        var startingQuestionMarkAdded = false;

        var queryString = new StringBuilder();

        foreach (var parameter in queryStringParams)
        {
            if (parameter.Value == null)
                continue;

            queryString.Append(startingQuestionMarkAdded ? '&' : '?');
            queryString.Append(parameter.Key);
            queryString.Append('=');
            queryString.Append(parameter.Value);
            startingQuestionMarkAdded = true;
        }

        return queryString.ToString();
    }
}
