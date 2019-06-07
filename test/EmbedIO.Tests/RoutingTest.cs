﻿using System;
using EmbedIO.Routing;
using NUnit.Framework;

namespace EmbedIO.Tests
{
    [TestFixture]
    public class RoutingTest
    {
        [TestCase("")] // Route is empty.
        [TestCase("abc")] // Route does not start with a slash.
        [TestCase("/abc/")] // Route must not end with a slash unless it is "/".
        [TestCase("/abc//def")] // Route must not contain consecutive slashes.
        [TestCase("/abc/{id")] // Route syntax error: unclosed parameter specification.
        [TestCase("/abc/{}")] // Route syntax error: empty parameter specification.
        [TestCase("/abc/{?}")] // Route syntax error: missing parameter name.
        [TestCase("/abc/{myp@rameter}")] // Route syntax error: parameter name contains one or more invalid characters.
        [TestCase("/abc/{id}/def/{id}")] // Route syntax error: duplicate parameter name.
        [TestCase("/abc/{id}{name}")] // Route syntax error: parameters must be separated by literal text.
        public void InvalidRoute_IsNotValid(string route)
        {
            RouteMatcher.ClearCache();

            Assert.IsFalse(Route.IsValid(route));
            Assert.Throws<FormatException>(() => RouteMatcher.Parse(route));
            Assert.IsFalse(RouteMatcher.TryParse(route, out _));
        }

        [TestCase("/")] // Root.
        [TestCase("/abc/def")] // No parameters.
        [TestCase("/abc/{id}")] // 1 parameter, takes a whole segment.
        [TestCase("/abc/{id?}")] // 1 optional parameter, takes a whole segment.
        [TestCase("/a{id}")] // 1 parameter, at start of segment.
        [TestCase("/{id}b")] // 1 parameter, at end of segment.
        [TestCase("/a{id}b")] // 1 parameter, mid-segment.
        [TestCase("/abc/{width}x{height}")] // 2 parameters, same segment.
        [TestCase("/abc/{width}/{height}")] // 2 parameters, different segments.
        [TestCase("/abc/{id}/{date?}")] // 2 parameters, different segments, 1 optional.
        public void ValidRoute_IsValid(string route)
        {
            RouteMatcher.ClearCache();

            Assert.IsTrue(Route.IsValid(route));
            Assert.DoesNotThrow(() => RouteMatcher.Parse(route));
            Assert.IsTrue(RouteMatcher.TryParse(route, out _));
        }

        [TestCase("/")] // Root.
        [TestCase("/abc/def")] // No parameters.
        [TestCase("/abc/{id}", "id")] // 1 parameter, takes a whole segment.
        [TestCase("/abc/{id?}", "id")] // 1 optional parameter, takes a whole segment.
        [TestCase("/a{id}", "id")] // 1 parameter, at start of segment.
        [TestCase("/{id}b", "id")] // 1 parameter, at end of segment.
        [TestCase("/a{id}b", "id")] // 1 parameter, mid-segment.
        [TestCase("/abc/{width}x{height}", "width", "height")] // 2 parameters, same segment.
        [TestCase("/abc/{width}/{height}", "width", "height")] // 2 parameters, different segments.
        [TestCase("/abc/{id}/{date?}", "id", "date")] // 2 parameters, different segments, 1 optional.
        public void RouteParameters_HaveCorrectNames(string route, params string[] parameterNames)
        {
            RouteMatcher.ClearCache();

            Assert.IsTrue(RouteMatcher.TryParse(route, out var matcher));
            Assert.AreEqual(parameterNames.Length, matcher.ParameterNames.Count);
            for (var i = 0; i < parameterNames.Length; i++)
                Assert.AreEqual(parameterNames[i], matcher.ParameterNames[i]);
        }
    }
}