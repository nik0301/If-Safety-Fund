using IFLike.DAL.Interfaces;
using IFLike.Domain;
using IFLike.Services.Interfaces;
using System;

namespace IFLike.Services.Implementation
{
    public class PollResultService : IPollResultService
    {
        private readonly IPollResultRepository _pollResultRepository;

        public PollResultService(IPollResultRepository pollResultRepository)
        {
            _pollResultRepository = pollResultRepository;
        }

        public bool AddVote(string email, int pollItemId, string countryCode)
        {
#if DEBUG
            var voteDEBUG = new PollResult()
                {
                    UserEmail = email,
                    PollItemId = pollItemId,
                    CountryCode = countryCode,
                    Created = DateTime.UtcNow
                };

                _pollResultRepository.Add(voteDEBUG);
                _pollResultRepository.Save();

                return true;
#endif

            var current = _pollResultRepository.GetBy(email, pollItemId);

            if (current == null)
            {
                var vote = new PollResult()
                {
                    UserEmail = email,
                    PollItemId = pollItemId,
                    CountryCode = countryCode,
                    Created = DateTime.UtcNow
                };

                _pollResultRepository.Add(vote);
                _pollResultRepository.Save();

                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetVoteCount(int pollResultId)
        {
            return _pollResultRepository.GetVotesCount(pollResultId);
        }
    }
}