using System;
using System.Net;
using EsriRestLibrary.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EsriRestLibrary.Core.Tasks
{
    public class IdentifyQueryTask<TGeo, TAttr>
    {
        private readonly string _token;

        public IdentifyQueryTask(string url, string token = null)
        {
            _token = token;
            _url = url;
        }

        private readonly string _url;

        public IdentifyResult<TGeo, TAttr> Execute(IdentifyRequest identifyRequest)
        {
            IdentifyResult<TGeo, TAttr> ret;
            try
            {
                var url = _url + "/identify";
                var client = new RestClient(url);
                var defaultProxy = new WebProxy {UseDefaultCredentials = true};
                client.Proxy = defaultProxy;

                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.Parameters.Clear();
                request.AddObject(identifyRequest);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");

                if (_token != null)
                    request.AddQueryParameter("token", _token);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new Exception("EsriResponseCode: " + response.StatusCode);


                var content = response.Content;
                ret = JsonConvert.DeserializeObject<IdentifyResult<TGeo, TAttr>>(content);
                if (ret.error != null) throw new Exception(ret.error.message);
            }
            catch (Exception e)
            {
                throw new Exception("Esri servisinde hata oluştu. Hata Kodu:-5000", e);
            }

            if (ret == null) throw new Exception("Esri servisinde hata oluştu. Hata Kodu:-5000");
            return ret;
        }
    }
}