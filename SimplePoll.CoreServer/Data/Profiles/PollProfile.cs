using AutoMapper;
using SimplePoll.Common;
using SimplePoll.Common.Data;
using SimplePoll.CoreServer.Data.Models;

namespace SimplePoll.CoreServer.Data.Profiles
{
    public class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<Poll, PollConfigurationDto>();
            CreateMap<PollElement, PollElementConfigurationDto>();
            CreateMap<PollElementValue, PollElementValueConfigurationDto>();
            CreateMap<PollAnswer, PollAnswerDto>();
            // TODO
            // CreateMap<Poll, PollResultsDto>();
        }
    }
}
