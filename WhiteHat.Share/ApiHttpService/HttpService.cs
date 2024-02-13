using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Net;
using WhiteHat.Share.Constant;
using WhiteHat.Share.Services.CoreServices;
using WhiteHat.Ui.Models.Models;

namespace WhiteHat.Share.ApiHttpService
{
    public interface IHttpService
    {
        Task<TResult> Get<TResult>(string uri);
        Task<TResult> Post<TValue, TResult>(string uri, TValue value);
        Task<TResult> Put<TValue, TResult>(string uri, TValue value);
        Task<TResult> Delete<TResult>(string uri);

        Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, bool bypassNetworkStateCheck = false);

        HttpRequestMessage GetRequest(HttpMethod method, string uri);
        HttpContent Content(Stream contentStream, string contentType, int contentLength);
        HttpContent Content<TValue>(TValue value);
    }

    public class HttpService : IHttpService
    {
        private readonly ILogger<HttpService> _logger;
        private readonly HttpClient _httpClient;
        private readonly INetworkStateService _networkState;
        private readonly ISessionState<AuthenticationModel> _session;
        private readonly NavigationManager _navigation;

        public HttpService(
            ILogger<HttpService> logger,
            HttpClient httpClient,
            INetworkStateService networkStateService,
            NavigationManager navigation,
            ISessionState<AuthenticationModel> session)
        {
            _logger = logger;
            _httpClient = httpClient;
            _networkState = networkStateService;
            _session = session;
            _navigation = navigation;
        }

        public async Task<TResult> Get<TResult>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<TResult>(request);
        }

        public async Task<TResult> Post<TValue, TResult>(
            string uri,
            TValue value
        )
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = Content(value)
            };

            return await SendRequest<TResult>(request);
        }

        public async Task<TResult> Put<TValue, TResult>(
            string uri,
            TValue value
        )
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri)
            {
                Content = Content(value)
            };

            return await SendRequest<TResult>(request);
        }

        public async Task<TResult> Delete<TResult>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return await SendRequest<TResult>(request);
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, bool bypassNetworkStateCheck = false)
        {
            if (!bypassNetworkStateCheck && !_networkState.IsOnline)
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }

            var session = await _session.Get(StorageKeyConstant.StorageKeyName);
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;

            if (session != null && DateTime.UtcNow > session.TokenExpireDate && session.IsActivate)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            if (session != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", session.Token);

            try
            {
                _logger.LogDebug("Sending {0} request to {1}", request.Method, request.RequestUri);

                return await _httpClient.SendAsync(request);
            }
            catch (Exception error)
            {
                _logger.LogInformation(error, "Error when sending request. {0}", error);
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            }
        }

        private async Task<TResult> SendRequest<TResult>(HttpRequestMessage request, bool bypassNetworkStateCheck = false)
        {
            using var response = await SendRequest(request, bypassNetworkStateCheck);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogDebug("{0} request to {1} was unauthorized", request.Method, request.RequestUri);
                await _session.DeleteAll();
                _navigation.NavigateTo("/", true);
                return default;
            }

            if (!response.IsSuccessStatusCode || response.Content.Headers.ContentLength == 0)
            {
                _logger.LogDebug("{0} request to {1} was not succesful", request.Method, request.RequestUri);
                return default;
            }

            _logger.LogDebug("{0} request to {1} was succesful", request.Method, request.RequestUri);
            return await response.Content.ReadFromJsonAsync<TResult>();
        }

        public HttpRequestMessage GetRequest(HttpMethod method, string uri)
        {
            return new HttpRequestMessage(method, uri);
        }

        public HttpContent Content(
            Stream contentStream,
            string contentType,
            int contentLength)
        {
            var content = new StreamContent(contentStream);

            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            content.Headers.ContentLength = contentLength;

            return content;
        }

        public HttpContent Content<TValue>(TValue value)
        {
            return JsonContent.Create(
                value,
                new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
                );
        }
    }
}
