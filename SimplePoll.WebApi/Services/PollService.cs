using System.Net;
using System.Text;
using System.Text.Json;
using SimplePoll.Common.Data;

namespace SimplePoll.WebApi.Services
{
    public class PollService : IPollService
    {
        private readonly HttpClient httpClient;

        public PollService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PollConfigurationDto?> GetPollAsync(int id)
        {
            var response = await httpClient.GetAsync($"/api/Poll/{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception("Server error");
            }

            var data = await response.Content.ReadFromJsonAsync<PollConfigurationDto>();

            return data;
        }

        public async Task<IEnumerable<PollConfigurationDto>?> GetPollsAsync()
        {
            var response = await httpClient.GetAsync("/api/Poll/all");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception("Server error");
            }

            var data = await response.Content.ReadFromJsonAsync<IEnumerable<PollConfigurationDto>>();

            return data;
        }

        public async Task<PollSubmissionResultDto?> Submit(int id, PollSubmissionRequestDto submission)
        {
            var body = new PollSubmissionDto()
            {
                PollId = id,
                UserName = submission.UserName,
                Answers = submission.Elements?.ToList(),
            };
            var serializedBody = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Poll", serializedBody);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new Exception("Server error");
            }

            var data = await response.Content.ReadFromJsonAsync<PollSubmissionResultDto>();

            return data;
        }
    }
}
