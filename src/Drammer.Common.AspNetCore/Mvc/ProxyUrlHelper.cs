using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Drammer.Common.AspNetCore.Mvc;

public sealed class ProxyUrlHelper : IUrlHelper
{
    private readonly IHttpContextAccessor _accessor;

    private readonly LinkGenerator _linkGenerator;

    public ProxyUrlHelper(IHttpContextAccessor accessor, LinkGenerator linkGenerator)
    {
        _accessor = accessor;
        _linkGenerator = linkGenerator;
    }

    public ActionContext ActionContext
    {
        get
        {
            if (_accessor.HttpContext == null)
            {
                ThrowHttpContextNotNull();
            }

            return new (_accessor.HttpContext, _accessor.HttpContext.GetRouteData(), new ActionDescriptor());
        }
    }

    public string? Action(UrlActionContext actionContext)
    {
        if (_accessor.HttpContext == null)
        {
            ThrowHttpContextNotNull();
        }

        return _linkGenerator.GetPathByAction(
            _accessor.HttpContext,
            actionContext.Action,
            actionContext.Controller,
            actionContext.Values);
    }

    public string? Content(string? contentPath)
    {
        if (_accessor.HttpContext == null)
        {
            ThrowHttpContextNotNull();
        }

        return _linkGenerator.GetPathByPage(_accessor.HttpContext, contentPath);
    }

    public bool IsLocalUrl(string? url)
    {
        throw new NotImplementedException();
    }

    public string? RouteUrl(UrlRouteContext routeContext)
    {
        if (_accessor.HttpContext == null)
        {
            ThrowHttpContextNotNull();
        }

        return _linkGenerator.GetPathByName(_accessor.HttpContext, routeContext.RouteName, routeContext.Values);
    }

    public string? Link(string? routeName, object? values)
    {
        if (_accessor.HttpContext == null)
        {
            ThrowHttpContextNotNull();
        }

        return _linkGenerator.GetUriByName(_accessor.HttpContext, routeName, values);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    [DoesNotReturn]
    private void ThrowHttpContextNotNull()
    {
        throw new InvalidOperationException(
            $"{nameof(IHttpContextAccessor)}.{nameof(_accessor.HttpContext)} cannot be null in {nameof(ProxyUrlHelper)}");
    }
}