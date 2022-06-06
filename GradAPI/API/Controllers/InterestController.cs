using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class InterestController : BaseAPIController
    {
        private readonly ILogger<InterestController> _logger;

        public InterestController(ILogger<InterestController> logger)
        {
            _logger = logger;
        }
    }
}