﻿using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers.api
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
    }
}