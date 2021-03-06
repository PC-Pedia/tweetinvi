using System.Threading.Tasks;
using Tweetinvi.Controllers.Search;
using Tweetinvi.Core.Events;
using Tweetinvi.Core.Iterators;
using Tweetinvi.Core.Web;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace Tweetinvi.Client.Requesters.V2
{
    public class SearchV2Requester : BaseRequester, ISearchV2Requester
    {
        private readonly ISearchV2Controller _searchV2Controller;

        public SearchV2Requester(
            ITwitterClient client,
            ITwitterClientEvents twitterClientEvents,
            ISearchV2Controller searchV2Controller) : base(client, twitterClientEvents)
        {
            _searchV2Controller = searchV2Controller;
        }

        public Task<ITwitterResult<SearchTweetsV2Response>> SearchTweetsAsync(ISearchTweetsV2Parameters parameters)
        {
            return ExecuteRequestAsync(request => _searchV2Controller.SearchTweetsAsync(parameters, request));
        }

        public ITwitterPageIterator<ITwitterResult<SearchTweetsV2Response>, string> GetSearchTweetsV2Iterator(ISearchTweetsV2Parameters parameters)
        {
            var request = TwitterClient.CreateRequest();
            return _searchV2Controller.GetSearchTweetsV2Iterator(parameters, request);
        }
    }
}