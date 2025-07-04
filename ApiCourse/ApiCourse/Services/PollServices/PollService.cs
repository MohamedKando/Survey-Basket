
using ApiCourse.Models;
using ApiCourse.Persistence;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Diagnostics.CodeAnalysis;

namespace ApiCourse.Services
{
    public class PollService : IPollService
    {
        private readonly AppDbContext _context;
        public PollService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationtoken)
        {
            var polls = await _context.Polls.AsNoTracking().ToListAsync();
            return polls.Adapt<IEnumerable<PollResponse>>();
        }
        public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationtoken)
        {
            var poll = await _context.Polls.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (poll == null) 
            {
                return Result.Failure<PollResponse>(PollErrors.EmptyPoll);
            }
            return Result.Success<PollResponse>(poll.Adapt<PollResponse>());

        }


        public async Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationtoken)
        {
            var isExsit = await _context.Polls.AnyAsync(x=>x.Title == request.Title, cancellationtoken);
            if (isExsit)
            {
                return Result.Failure<PollResponse>(PollErrors.TitleExist);
            }
            var poll = request.Adapt<Poll>();
            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
            var response = poll.Adapt<PollResponse>();
            return Result.Success(response);
        }
        public async Task<Result> UpdateAsync(int id, PollRequest poll, CancellationToken cancellationtoken = default)
        {

            var currentPoll = await _context.Polls.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            if (currentPoll == null)
            {
                return Result.Failure(PollErrors.EmptyPoll);
            }
            var isExsit = await _context.Polls.AnyAsync(x => x.Title == poll.Title && x.Id != id, cancellationtoken);
            if (isExsit)
            {
                return Result.Failure<PollResponse>(PollErrors.TitleExist);
            }
            currentPoll.Title = poll.Title;
            currentPoll.Summary = poll.Summary;
            currentPoll.StartsAt = poll.StartsAt;
            currentPoll.EndsAt = poll.EndsAt;
            _context.Update(currentPoll);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationtoken = default)
        {
            var currentPoll = await _context.Polls.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (currentPoll == null)
            {
                return Result.Failure(PollErrors.EmptyPoll);
            }
            _context.Remove(currentPoll);
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> TogglePublishStatuesAsync(int id, CancellationToken cancellationtoken = default)
        {
            var poll = await _context.Polls.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            if (poll == null)
            {
                return Result.Failure(PollErrors.EmptyPoll);
            }
            poll.IsPublished = !poll.IsPublished;
  
            _context.Update(poll);
            await _context.SaveChangesAsync();
            return Result.Success();

        }
        public async Task<Result> ClearAllPollsAsync(CancellationToken cancellationToken)
        {
            _context.Polls.RemoveRange(_context.Polls); // delete all rows
            await _context.SaveChangesAsync(cancellationToken);

            // Reset identity (works with SQL Server)
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Polls', RESEED, 0)", cancellationToken);

            return Result.Success();
        }
    }
}
