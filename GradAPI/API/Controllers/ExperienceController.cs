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
    public class ExperienceController : BaseAPIController
    {
       private readonly ILogger<ExperienceController> _logger;

        public ExperienceController(ILogger<ExperienceController> logger)
        {
            _logger = logger;
        }
    }
}