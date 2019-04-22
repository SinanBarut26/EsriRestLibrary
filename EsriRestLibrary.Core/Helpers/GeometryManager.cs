using System;
using System.Collections.Generic;
using System.Net;
using Entity.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EsriRestLibrary.Core.Helpers
{
    internal static class GeometryManager
    {
        internal static IEnumerable<ApplyEditsResult> FeatureTask(string featureUrl, FeatureRequest featureRequest,
            string token)
        {
            var client = new RestClient(featureUrl + "/applyEdits");
            var defaultProxy = new WebProxy {UseDefaultCredentials = true};
            client.Proxy = defaultProxy;

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.Parameters.Clear();
            request.AddObject(featureRequest);
            request.AddQueryParameter("token", token);

            //request.AddHeader("Content-Type", "application/form-data; charset=utf-8");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=utf-8");
            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new Exception("Esri Error Response Code: " + response.StatusCode);

            var content = response.Content;

            try
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<ApplyEditsResult>>(content);
                if (result == null) throw new Exception("No Response Esri Service: Error Code: #-1002#");
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Feature Service. Error Code : #-1002#", e);
            }
        }
    }
}