using AgileManagement.Application.dtos.backlog;
using AgileManagement.Application.services.backlog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBacklogItemsController : ControllerBase
    {
        private readonly ProductBackLogItemTaskCreateService _productBackLogItemTaskCreateService;

        public ProductBacklogItemsController(ProductBackLogItemTaskCreateService productBackLogItemTaskCreateService)
        {
            _productBackLogItemTaskCreateService = productBackLogItemTaskCreateService;
        }

        [HttpPost("addTask")]
        public IActionResult AddTask(ProductBacklogItemTaskCreateRequestDto request)
        {

           var response = _productBackLogItemTaskCreateService.OnProcess(request);

            return Ok(response);
        }
    }
}
