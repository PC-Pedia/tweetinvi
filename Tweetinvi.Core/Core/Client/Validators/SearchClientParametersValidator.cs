using System;
using Tweetinvi.Exceptions;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Parameters.Enum;

namespace Tweetinvi.Core.Client.Validators
{
    public interface ISearchClientParametersValidator
    {
        void Validate(ISearchTweetsParameters parameters);
    }

    public class SearchClientParametersValidator : ISearchClientParametersValidator
    {
        private readonly ITwitterClient _client;
        private readonly ISearchClientRequiredParametersValidator _searchClientRequiredParametersValidator;

        public SearchClientParametersValidator(
            ITwitterClient client,
            ISearchClientRequiredParametersValidator searchClientRequiredParametersValidator)
        {
            _client = client;
            _searchClientRequiredParametersValidator = searchClientRequiredParametersValidator;
        }

        public void Validate(ISearchTweetsParameters parameters)
        {
            _searchClientRequiredParametersValidator.Validate(parameters);

            var isSearchQuerySet = !string.IsNullOrEmpty(parameters.Query);
            var isSearchQueryValid = IsSearchQueryValid(parameters.Query);
            var isGeoCodeSet = IsGeoCodeValid(parameters.GeoCode);
            var isEntitiesTypeSet = parameters.Filters != TweetSearchFilters.None;

            var isSearchValid = (isSearchQuerySet && isSearchQueryValid) || isGeoCodeSet || isEntitiesTypeSet;
            if (!isSearchValid)
            {
                throw new ArgumentException("At least one of the required parameters needs to be valid (query, geocode or filter).");
            }

            var maxPageSize = _client.ClientSettings.Limits.TIMELINE_HOME_PAGE_MAX_PAGE_SIZE;
            if (parameters.PageSize > maxPageSize)
            {
                throw new TwitterArgumentLimitException($"{nameof(parameters)}.{nameof(parameters.PageSize)}", maxPageSize, nameof(_client.ClientSettings.Limits.TIMELINE_HOME_PAGE_MAX_PAGE_SIZE), "page size");
            }
        }

        private bool IsSearchQueryValid(string searchQuery)
        {
            // We might want to restrict the size to 1000 characters as indicated in the documentation
            return !string.IsNullOrWhiteSpace(searchQuery);
        }

        private bool IsGeoCodeValid(IGeoCode geoCode)
        {
            return geoCode != null;
        }
    }
}