using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        [HttpPost("result")]
        public IActionResult Result([FromForm] string orderId, [FromForm] string amount, [FromForm] string result)
        {

            Dictionary<string, string> dictValues = new Dictionary<string, string>();
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                dictValues.Add(key, HttpContext.Request.Form[key]);
            }
            return Ok(dictValues);
            /*
            return Ok("Order Id : "+orderId+"\n"+
                        "AMount : "+amount+"\n"+
                        "result : "+result+"\n");
            */
        }

    }
}
