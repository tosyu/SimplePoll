using AutoMapper;
using SimplePoll.Common.Data;
using SimplePoll.CoreServer.Data.Models;
using SimplePoll.CoreServer.Data.Repositories;
using SimplePoll.CoreServer.Exceptions;

namespace SimplePoll.CoreServer.Services
{
    public class PollService : IPollService
    {
        private readonly IPollRepository pollRepository;
        private readonly ISubmissionRepository submitionRepository;
        private readonly IMapper mapper;

        public PollService(IPollRepository pollRepository, ISubmissionRepository submitionRepository, IMapper mapper)
        {
            this.pollRepository = pollRepository;
            this.submitionRepository = submitionRepository;
            this.mapper = mapper;
        }

        public async Task<PollConfigurationDto> GetPollAsync(int id)
        {
            var poll = await pollRepository.GetPollAsync(id);

            if (poll == null) {
                throw new NotFoundException();
            }

            return mapper.Map<PollConfigurationDto>(poll);
        }

        public async Task<IEnumerable<PollConfigurationDto>> GetPollsAsync()
        {
            var polls = await pollRepository.GetPollsAsync();
            return mapper.Map<IEnumerable<PollConfigurationDto>>(polls);
        }

        public async Task<PollSubmissionResultDto> SubmitPollResults(PollSubmissionDto pollSubmission)
        {
            var poll = await pollRepository.GetPollAsync(pollSubmission.PollId);

            if (poll == null)
            {
                throw new NotFoundException();
            }

            var newSubmission = new PollSubmission()
            {
                Poll = poll,
                PollId = pollSubmission.PollId,
                SubmissionUser = pollSubmission.UserName
            };

            var answers = new List<PollAnswer>();

            if (pollSubmission.Answers != null)
            {
                foreach (var answerDto in pollSubmission.Answers)
                {
                    var element = poll.Elements?.FirstOrDefault(p =>  p.Id == answerDto.Id);
                    answers.Add(new PollAnswer()
                    {
                        PollElement = element,
                        PollSubmission = newSubmission,
                        Value = answerDto.Value
                    });
                }
                newSubmission.Answers = answers;
            }

            submitionRepository.Create(newSubmission);

            try
            {
                await submitionRepository.SaveAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new PollSubmissionResultDto()
            {
                CreatedAt = newSubmission.CreatedAt.ToString(),
                PollResultId = newSubmission.Id
            };
        }
    }
}
