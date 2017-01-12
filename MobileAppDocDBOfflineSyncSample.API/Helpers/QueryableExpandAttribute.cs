using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class QueryableExpandAttribute : ActionFilterAttribute
{
    private const string ODataExpandOption = "$expand=";

    public QueryableExpandAttribute(string expand)
    {
        this.AlwaysExpand = expand;
    }

    public string AlwaysExpand { get; set; }

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        HttpRequestMessage request = actionContext.Request;
        string query = request.RequestUri.Query.Substring(1);
        var parts = query.Split('&').ToList();
        bool foundExpand = false;
        for (int i = 0; i < parts.Count; i++)
        {
            string segment = parts[i];
            if (segment.StartsWith(ODataExpandOption, StringComparison.Ordinal))
            {
                foundExpand = true;
                parts[i] += "," + this.AlwaysExpand;
                break;
            }
        }

        if (!foundExpand)
        {
            parts.Add(ODataExpandOption + this.AlwaysExpand);
        }

        UriBuilder modifiedRequestUri = new UriBuilder(request.RequestUri);
        modifiedRequestUri.Query = string.Join("&",
        parts.Where(p => p.Length > 0));
        request.RequestUri = modifiedRequestUri.Uri;
        base.OnActionExecuting(actionContext);
    }
}