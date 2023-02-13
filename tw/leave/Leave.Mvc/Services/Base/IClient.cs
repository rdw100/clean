using System.Net.Http;

namespace Leave.Mvc.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
}
