﻿using System;
using System.Web;
using System.Web.Mvc;

namespace WAVE.Website.Filters
{
    public class CacheFilterAttribute : ActionFilterAttribute
    {
        public CacheFilterAttribute()
        {
            Duration = 60;
        }

        /// <summary>
        ///     Gets or sets the cache duration in seconds. The default is 10 seconds.
        /// </summary>
        /// <value>The cache duration in seconds.</value>
        public int Duration { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Duration <= 0) return;

            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromSeconds(Duration);

            cache.SetCacheability(HttpCacheability.Public);
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }
    }
}