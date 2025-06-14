using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MSESG.CargoCare.Web.Handler
{
    public class ErrorResult : ActionResult
    {
        /// <summary>Gets the HTTP status code.</summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.AspNetCore.Mvc.StatusCodeResult" /> class
        /// with the given <paramref name="statusCode" />.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        public ErrorResult(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        /// <inheritdoc />
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");


            var r = new HttpResponseMessage();

           // context.HttpContext.Response
        }

    }
}
