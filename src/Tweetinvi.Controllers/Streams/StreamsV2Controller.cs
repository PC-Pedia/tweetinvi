using System.Threading.Tasks;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.V2;
using Tweetinvi.Parameters.V2;

namespace Tweetinvi.Controllers.Streams
{
    public interface IStreamsV2Controller
    {
        Task<ITwitterResult<FilteredStreamRulesV2Response>> GetRulesForFilteredStreamV2Async(IGetRulesForFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> AddRulesToFilteredStreamAsync(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> DeleteRulesFromFilteredStreamAsync(IDeleteRulesFromFilteredStreamV2Parameters parameters, ITwitterRequest request);
        Task<ITwitterResult<FilteredStreamRulesV2Response>> TestFilteredStreamRulesV2Async(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request);
    }

    public class StreamsV2Controller : IStreamsV2Controller
    {
        private readonly IStreamsV2QueryExecutor _streamsV2QueryExecutor;

        public StreamsV2Controller(IStreamsV2QueryExecutor streamsV2QueryExecutor)
        {
            _streamsV2QueryExecutor = streamsV2QueryExecutor;
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> GetRulesForFilteredStreamV2Async(IGetRulesForFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            return _streamsV2QueryExecutor.GetRulesForFilteredStreamV2Async(parameters, request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> AddRulesToFilteredStreamAsync(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            return _streamsV2QueryExecutor.AddRulesToFilteredStreamAsync(parameters, request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> DeleteRulesFromFilteredStreamAsync(IDeleteRulesFromFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            return _streamsV2QueryExecutor.DeleteRulesFromFilteredStreamAsync(parameters, request);
        }

        public Task<ITwitterResult<FilteredStreamRulesV2Response>> TestFilteredStreamRulesV2Async(IAddRulesToFilteredStreamV2Parameters parameters, ITwitterRequest request)
        {
            return _streamsV2QueryExecutor.TestFilteredStreamRulesV2Async(parameters, request);
        }
    }
}