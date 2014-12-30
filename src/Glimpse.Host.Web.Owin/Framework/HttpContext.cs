﻿using System;
using System.Collections.Generic;
using Glimpse.Web;

namespace Glimpse.Host.Web.Owin
{
    public class HttpContext : IHttpContext
    {
        private readonly string _itemsKey = "glimpse.RequestStore";
        private readonly Microsoft.Owin.IOwinContext _context;
        private readonly IHttpRequest _request;
        private readonly IHttpResponse _response;
        private readonly IServiceProvider _serviceProvider;

        public HttpContext(IDictionary<string, object> environement, IServiceProvider serviceProvider)
        {
            _context = new Microsoft.Owin.OwinContext(environement);
            _request = new HttpRequest(_context.Request);
            _response = new HttpResponse(_context.Response);
            _serviceProvider = serviceProvider;
        }

        public IHttpRequest Request
        {
            get { return _request;  }
        }

        public IHttpResponse Response
        {
            get { return _response; }
        }

        public IDictionary<string, object> Items
        {
            get
            {
                if (_context.Environment.ContainsKey(_itemsKey))
                {
                    return (IDictionary<string, object>)_context.Environment[_itemsKey];
                }

                var result = new Dictionary<string, object>();
                _context.Environment[_itemsKey] = result;

                return result;
            }
        }

        public IServiceProvider ApplicationServices
        {
            get { return _serviceProvider; }
        }
    }
}