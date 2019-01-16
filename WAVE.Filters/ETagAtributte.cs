using System;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace WAVE.Website.Filters
{
    public class ETagAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Filter = new ETagFilter(filterContext.HttpContext.Response,
                filterContext.HttpContext.Request);
        }
    }

    public class ETagFilter : MemoryStream
    {
        private readonly Stream _filter;
        private readonly HttpRequestBase _request;
        private readonly HttpResponseBase _response;

        public ETagFilter(HttpResponseBase response, HttpRequestBase request)
        {
            _response = response;
            _request = request;
            _filter = response.Filter;
        }

        private string GetToken(Stream stream)
        {
            byte[] checksum = MD5.Create().ComputeHash(stream);
            return Convert.ToBase64String(checksum, 0, checksum.Length);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            string token = GetToken(new MemoryStream(data));

            string clientToken = _request.Headers["If-None-Match"];

            if (token != clientToken)
            {
                _response.Headers["ETag"] = token;
                _filter.Write(data, 0, count);
            }
            else
            {
                _response.SuppressContent = true;
                _response.StatusCode = 304;
                _response.StatusDescription = "Not Modified";
                _response.Headers["Content-Length"] = "0";
            }
        }
    }
}