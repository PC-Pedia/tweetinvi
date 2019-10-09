﻿using System.Threading.Tasks;
using FakeItEasy;
using Tweetinvi;
using Tweetinvi.Controllers.Tweet;
using Tweetinvi.Core.QueryGenerators;
using Tweetinvi.Core.Web;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Parameters;
using Xunit;
using xUnitinvi.TestHelpers;

namespace xUnitinvi.ClientActions.TweetsClient
{
    public class TweetQueryExecutorTests
    {
        public TweetQueryExecutorTests()
        {
            _fakeBuilder = new FakeClassBuilder<TweetQueryExecutor>();
            _fakeTweetQueryGenerator = _fakeBuilder.GetFake<ITweetQueryGenerator>().FakedObject;
            _fakeTwitterAccessor = _fakeBuilder.GetFake<ITwitterAccessor>().FakedObject;
        }

        private readonly FakeClassBuilder<TweetQueryExecutor> _fakeBuilder;
        private readonly ITweetQueryGenerator _fakeTweetQueryGenerator;
        private readonly ITwitterAccessor _fakeTwitterAccessor;

        private TweetQueryExecutor CreateUserQueryExecutor()
        {
            return _fakeBuilder.GenerateClass();
        }

        [Fact]
        public async Task GetTweet_ReturnsFavoritedTweets()
        {
            // Arrange
            var queryExecutor = CreateUserQueryExecutor();
            var expectedQuery = TestHelper.GenerateString();

            var parameters = new GetTweetParameters(42);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryGenerator.GetTweetQuery(parameters, It.IsAny<TweetMode?>())).Returns(expectedQuery);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<ITweetDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetTweet(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, expectedQuery);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
        
        [Fact]
        public async Task PublishTweet_ReturnsFavoritedTweets()
        {
            // Arrange
            var queryExecutor = CreateUserQueryExecutor();
            var expectedQuery = TestHelper.GenerateString();

            var parameters = new PublishTweetParameters("hello");
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO>>();

            A.CallTo(() => _fakeTweetQueryGenerator.GetPublishTweetQuery(parameters, It.IsAny<TweetMode?>())).Returns(expectedQuery);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<ITweetDTO>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.PublishTweet(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, expectedQuery);
            Assert.Equal(HttpMethod.POST, request.Query.HttpMethod);
        }
        
        [Fact]
        public async Task GetFavoriteTweets_ReturnsFavoritedTweets()
        {
            // Arrange
            var queryExecutor = CreateUserQueryExecutor();
            var userDTO = A.Fake<IUserDTO>();
            var expectedQuery = TestHelper.GenerateString();

            var parameters = new GetFavoriteTweetsParameters(userDTO);
            var request = A.Fake<ITwitterRequest>();
            var expectedResult = A.Fake<ITwitterResult<ITweetDTO[]>>();

            A.CallTo(() => _fakeTweetQueryGenerator.GetFavoriteTweetsQuery(parameters, It.IsAny<TweetMode?>())).Returns(expectedQuery);
            A.CallTo(() => _fakeTwitterAccessor.ExecuteRequest<ITweetDTO[]>(request)).Returns(expectedResult);

            // Act
            var result = await queryExecutor.GetFavoriteTweets(parameters, request);

            // Assert
            Assert.Equal(result, expectedResult);
            Assert.Equal(request.Query.Url, expectedQuery);
            Assert.Equal(HttpMethod.GET, request.Query.HttpMethod);
        }
    }
}