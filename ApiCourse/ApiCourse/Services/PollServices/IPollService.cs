namespace ApiCourse.Services
{
    public interface IPollService
    {
        Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationtoken);

        Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationtoken);

        Task<Result<PollResponse>> AddAsync(PollRequest poll, CancellationToken cancellationtoken = default);

        Task<Result> UpdateAsync(int id, PollRequest poll, CancellationToken cancellationtoken = default);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationtoken = default);

        Task<Result> TogglePublishStatuesAsync(int id, CancellationToken cancellationtoken = default);
        public Task<Result> ClearAllPollsAsync(CancellationToken cancellationToken = default);
    }
}
