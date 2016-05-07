using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Common.Helpers
{
    public static class HttpRequestHelper
    {
        public static T Get<T>(string baseUri, string relativeUri)
        {
            // Get the absolute endpoint, adding any required oData parameters to query string
            Uri absoluteUri = GetAbsoluteEndpoint(baseUri, relativeUri);

            return Get<T>(absoluteUri);
        }

        public static T Get<T>(Uri absoluteUri)
        {
            T result = default(T);

            // Removed the using statment for httpclient to pass integration test - need to keep an eye on it though
            HttpClient httpClient = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false });

            // Create the request
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, absoluteUri);
            request.Headers.Add("Accept", "application/json");

            // Make the request  - HttpCompletionOption.ResponseHeadersRead is used to avoid memory leak
            //return httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            // Make the call and get the response
            HttpResponseMessage response = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
          

            // Read the result. This will deserialise the incoming response content
            if (response.IsSuccessStatusCode && response.Content != null && response.Content.Headers.ContentType != null)
            {
                // Get the type of media and use the formatter to get the content out of response
                MediaTypeFormatter formatter = GetMediaTypeFormatter(response.Content.Headers.ContentType.MediaType);

                result = response.Content.ReadAsAsync<T>(new List<MediaTypeFormatter> { formatter }).Result;
            }

            // Return the response containing the de-serialised result
            return result;
        }

        public static HttpResponseMessage Post<T>(
           string baseUri,
           string relativeUri,
           T content)
        {
            return Post(baseUri, relativeUri, content, null);
        }

        public static HttpResponseMessage Post<T>(
           string baseUri,
           string relativeUri,
           T content,
           int? userId)
        {
            return PostRequest(baseUri, relativeUri, content, userId, null);
        }

        public static HttpResponseMessage Put<T>(
           string baseUri,
           string relativeUri,
           T content)
        {
            return Put(baseUri, relativeUri, content, null);
        }

        public static HttpResponseMessage Put<T>(
           string baseUri,
           string relativeUri,
           T content,
           int? userId,
           bool serializeTypeNames = false)
        {
            HttpResponseMessage response = null;

            // Get the absolute endpoint, adding any required oData parameters to query string
            Uri absoluteUri = GetAbsoluteEndpoint(baseUri, relativeUri);

            using (HttpClient httpClient = new HttpClient())
            {
                // Create the PUT request
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, absoluteUri);
                request.Headers.Add("Accept", "application/json");

                // if content is null, then send a PUT request with no content.
                if (content == null)
                {
                    request.Content = new StringContent(string.Empty);
                }
                else
                {
                    string mediaType = string.Empty;

                    if (request.Headers.Accept != null && request.Headers.Accept.Count > 0)
                    {
                        mediaType = request.Headers.Accept.FirstOrDefault().MediaType;
                    }

                    // Serialise the content and add to the request
                    request.Content = new ObjectContent<T>(content, GetMediaTypeFormatter(mediaType));
                }

                // Perform the PUT - HttpCompletionOption.ResponseHeadersRead is used to avoid memory leak
                response = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            }

            return response;
        }


        private static HttpResponseMessage PostRequest<T>(
            string baseUri,
            string relativeUri,
            T content,
            int? userId,
            int? timeoutSeconds)
        {
            HttpResponseMessage response = null;

            // Get the absolute endpoint, adding any required oData parameters to query string
            Uri absoluteUri = GetAbsoluteEndpoint(baseUri, relativeUri);

            using (HttpClient httpClient = new HttpClient())
            {
                if (timeoutSeconds.HasValue)
                {
                    httpClient.Timeout = new TimeSpan(0, 0, timeoutSeconds.Value);
                }

                // Create the POST request
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, absoluteUri);
                request.Headers.Add("Accept", "application/json");


                // if content is null, then send a POST request with no content.
                if (content == null)
                {
                    request.Content = new StringContent(string.Empty);
                }
                else
                {
                    string mediaType = string.Empty;

                    if (request.Headers.Accept != null && request.Headers.Accept.Count > 0)
                    {
                        mediaType = request.Headers.Accept.FirstOrDefault().MediaType;
                    }

                    // Serialise the content and add to the request
                    request.Content = new ObjectContent<T>(content, GetMediaTypeFormatter(mediaType));
                }

                // Perform the POST - HttpCompletionOption.ResponseHeadersRead is used to avoid memory leak
                response = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            }

            return response;

        }
        private static MediaTypeFormatter GetMediaTypeFormatter(string mediaType)
        {
            JsonMediaTypeFormatter jsonMediaTypeFormatter = null;
            MediaTypeFormatter mediaTypeFormatter = null;

            switch (mediaType)
            {
                case "application/json":
                    jsonMediaTypeFormatter = new JsonMediaTypeFormatter();

                    mediaTypeFormatter = jsonMediaTypeFormatter;
                    break;
            }

            return mediaTypeFormatter;
        }

        private static Uri GetAbsoluteEndpoint(
            string baseUri,
            string relativeUri)
        {
            // Construct the absolute end point
            if (!string.IsNullOrEmpty(relativeUri) && relativeUri.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                // Remove the preceding backslash
                relativeUri = relativeUri.Substring(1);
            }

            if (!string.IsNullOrEmpty(baseUri) && baseUri.Trim().EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                // Remove the trailing backslash
                baseUri = baseUri.Substring(0, baseUri.Trim().Length - 1);
            }

            return new Uri(string.Format("{0}/{1}", baseUri, relativeUri));
        }
    }
}
