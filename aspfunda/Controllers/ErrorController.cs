using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aspfunda.Controllers
{

    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
           // var StatusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (StatusCode)
            {
                case 404: 
                    ViewBag.ErrorMessage = "Sorry, the message you requested" +
                        " could not be found";
                   // logger.LogWarning($"404 Error Occur.path={StatusCodeResult.OriginalPath}" + $"{StatusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceeptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //ViewBag.ExceptionPath = exceeptionDetails.Path;
            //ViewBag.Exceptionmessage = exceeptionDetails.Error.Message;
            //ViewBag.Stacktrace = exceeptionDetails.Error.StackTrace;
            logger.LogError($"The Path{exceeptionDetails.Path} threw the exception "+  $"{exceeptionDetails.Error}");
            return View("Error");
        }

    }
}
