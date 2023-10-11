using FormCollectionApi.Interfaces;
using FormCollectionApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FormCollectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackFormController : ControllerBase
    {
        private readonly IFormService formService;

        public FeedBackFormController(IFormService formService)
        {
            this.formService = formService;
        }

        // GET api/<FeedBackFormController>/5
        [HttpGet]
        [Route("products")]
        public Task<IActionResult> GetProducts()
        {
            return formService.GetProductsList();
        }

        // POST api/<FeedBackFormController>
        [HttpPost]
        public Task<IActionResult> AddFeedBack([FromBody] UserFeedBackForm value)
        {
            return formService.SaveUserFeedBack(value);
        }

       
    }
}
