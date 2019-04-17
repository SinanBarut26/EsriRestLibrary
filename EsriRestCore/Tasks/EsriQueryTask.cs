using System;
using System.Net;
using EsriRestLibrary.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EsriRestLibrary.Core.Tasks
{
    public class EsriQueryTask<TGeo, TAttr>
    {
        private readonly string _token;

        public EsriQueryTask(string url, string token = null)
        {
            _token = token;
            _url = url;
        }

        private readonly string _url;

        public QueryResult<TGeo, TAttr> Execute(QueryRequest queryRequest)
        {
            QueryResult<TGeo, TAttr> ret;
            try
            {
                var client = new RestClient(_url + "/query");
                var defaultProxy = new WebProxy {UseDefaultCredentials = true};
                client.Proxy = defaultProxy;

                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.Parameters.Clear();
                request.AddObject(queryRequest);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

                if (_token != null)
                    request.AddQueryParameter("token", _token);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new Exception("Esri Error Response Code: " + response.StatusCode);


                var content = response.Content;
                ret = JsonConvert.DeserializeObject<QueryResult<TGeo, TAttr>>(content);
                if (ret.error != null) throw new Exception(ret.error.message);
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Service. Error Code : #-1002# ", e);
            }

            return ret;
        }
    }
}