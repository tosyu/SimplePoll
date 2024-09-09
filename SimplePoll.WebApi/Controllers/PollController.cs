using Microsoft.AspNetCore.Mvc;
using SimplePoll.Common.Data;
using SimplePoll.WebApi.Services;

namespace SimplePoll.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IPollService pollService;

        public PollController(IPollService pollService)
        {
            this.pollService = pollService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await pollService.GetPollsAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll(int id)
        {
            var result = await pollService.GetPollAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Submit(int id, [FromBody] PollSubmissionRequestDto submission)
        {
            var result = await pollService.Submit(id, submission);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
