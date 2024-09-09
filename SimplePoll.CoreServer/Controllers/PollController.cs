using Microsoft.AspNetCore.Mvc;
using SimplePoll.Common.Data;
using SimplePoll.CoreServer.Exceptions;
using SimplePoll.CoreServer.Services;

namespace SimplePoll.CoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IPollService pollService;

        public PollController(IPollService pollService)
        {
            this.pollService = pollService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<PollConfigurationDto>> GetPolls()
        {
            return await pollService.GetPollsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll(int id)
        {
            try
            {
                var poll = await pollService.GetPollAsync(id);
                return Ok(poll);
            } catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] PollSubmissionDto submission)
        {
            var results = await pollService.SubmitPollResults(submission);

            return Ok(results);
        }
    }
}
