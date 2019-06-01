﻿using System.Threading;
using System.Threading.Tasks;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace EmbedIO.Tests.TestObjects
{
    public class TestControllerWithConstructor : WebApiController
    {
        public const string CustomHeader = "X-Custom";

        public TestControllerWithConstructor(IHttpContext context, CancellationToken cancellationToken, string name = "Test")
            : base(context, cancellationToken)
        {
            WebName = name;
        }

        public string WebName { get; set; }

        [RouteHandler(HttpVerbs.Get, "/name")]
        public Task<bool> GetName()
        {
            Response.NoCache();
            return Ok(WebName);
        }

        [RouteHandler(HttpVerbs.Get, "/namePublic")]
        public Task<bool> GetNamePublic()
        {
            Response.AddHeader("Cache-Control", "public");
            return Ok(WebName);
        }

        protected override void OnBeforeHandler() => Response.AddHeader(CustomHeader, WebName);
    }
}