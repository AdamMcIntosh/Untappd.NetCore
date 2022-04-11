using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Untappd.NetCore.Authentication;
using Untappd.NetCore.Exception;

namespace Untappd.NetCore.Request
{
    public sealed partial class Repository
    {
        internal RestClient Client;
        internal RestRequest Request;
        public bool FailFast { get; set; }

        /// <summary>
        /// Event to listen to when failFast is set to false
        /// This allows you to capture the excpetion, before its swallowed
        /// </summary>
        public event UnhandledExceptionEventHandler OnExceptionThrown;

        /// <summary>
        /// Make a repository
        /// </summary>
        /// <param name="failFast">Should we throw exceptions? or just return null</param>
        /// <param name="timeout">Set a timeout in milliseconds</param>
        public Repository(bool failFast = true, int timeout = 0)
        {
            Client = new RestClient(Constants.BaseRequestString);
            Request = new RestRequest
            {
                Timeout = timeout
            };
            FailFast = failFast;
        }


        internal Repository ConfigureRequest(string endPoint, IDictionary<string, object> bodyParameters, Method webMethod = Method.Get)
        {
            Request.Resource = endPoint;
            Request.Method = webMethod;
            if (Request.Parameters.Count > 0)
            {
                foreach (var requestParameter in Request.Parameters)
                {
                    Request.Parameters.RemoveParameter(requestParameter);
                }
            }

            if (bodyParameters == null) return this;
            foreach (var param in bodyParameters)
            {
                Request.AddParameter(param.Key, param.Value.ToString());
            }
            return this;
        }

        internal Repository ConfigureRequest(IUntappdCredentials credentials, string endPoint, IDictionary<string, object> bodyParameters = null, Method webMethod = Method.Get)
        {
            ConfigureRequest(endPoint, bodyParameters, webMethod);
            foreach (var untappdCredential in credentials.AuthenticationData)
            {
                Request.AddParameter(untappdCredential.Key, untappdCredential.Value);
            }
            return this;
        }

        private async Task<TResult> ExecuteRequest<TResult>() where TResult : class
        {
            return ProcessExecution<TResult>(await Client.ExecuteAsync(Request));
        }

        private async Task<TResult> ExecuteRequestAsync<TResult>() where TResult : class
        {
            return ProcessExecution<TResult>(await Client.ExecuteAsync(Request));
        }

        private TResult ProcessExecution<TResult>(RestResponse response)
            where TResult : class
        {
            //if the return type is not 200 throw errors
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var excpetion = new HttpErrorException(Request, response);
                var eventThrow = OnExceptionThrown;

                if (eventThrow != null)
                {
                    eventThrow(this, new UnhandledExceptionEventArgs(excpetion, FailFast));
                }
                if (FailFast)
                {
                    throw excpetion;
                }
                return null;
            }
            //try to deserialize
            try
            {
                return JsonConvert.DeserializeObject<TResult>(response.Content);
            }
            catch (System.Exception e)
            {
                var eventThrow = OnExceptionThrown;

                if (eventThrow != null)
                {
                    eventThrow(this, new UnhandledExceptionEventArgs(e, FailFast));
                }
                if (FailFast)
                {
                    throw;
                }

                return null;
            }
        }
    }
}
