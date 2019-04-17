using System;
using System.Linq;
using System.Net;
using EsriRestLibrary.Core.Enums;
using EsriRestLibrary.Core.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EsriRestLibrary.Core.Helpers
{
    internal static class GeometryManager
    {
        internal static Result FeatureTask(string featureUrl, FeatureRequest featureRequest, ApplyEditsTypes applyEditsType,
            string token)
        {
            var client = new RestClient(featureUrl + "/applyEdits");
            var defaultProxy = new WebProxy { UseDefaultCredentials = true };
            client.Proxy = defaultProxy;

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json,
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

            ApplyEditsResult result;
            try
            {
                result = JsonConvert.DeserializeObject<ApplyEditsResult>(content);
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Feature Service. Error Code : #-1002#", e);
            }
            if (result == null) throw new Exception("No Response Esri Service: Error Code: #-1002#");
            if (result.error != null) throw new Exception(result.error.message);
            if (result.addResults.Any()) return result.addResults.FirstOrDefault();
            return result.updateResults.Any() ? result.updateResults.FirstOrDefault() : result.deleteResults.FirstOrDefault();
        }


        internal static AddFeatureResult AddFeatureTask(string featureUrl, AddFeatureRequest addFeatureRequest,
            string token = null)
        {
            AddFeatureResult ret;

            try
            {
                var client = new RestClient(featureUrl + "/addFeatures");
                var defaultProxy = new WebProxy { UseDefaultCredentials = true };
                client.Proxy = defaultProxy;

                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                };
                request.AddParameter("features", addFeatureRequest, ParameterType.RequestBody);
                request.AddParameter("f", "pjson");

                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                if (token != null)
                    request.AddQueryParameter("token", token);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new Exception("Esri Error Response Code: " + response.StatusCode);


                var content = response.Content;
                ret = JsonConvert.DeserializeObject<AddFeatureResult>(content);

                if (ret.error != null) throw new Exception(ret.error.message);
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Feature Service. Error Code : #-1002#", e);
            }

            return ret;
        }

        internal static UpdateFeatureResult UpdateFeatureTask(string featureUrl,
            UpdateFeatureRequest updateFeatureRequest, string token = null)
        {
            UpdateFeatureResult ret;
            try
            {
                var client = new RestClient(featureUrl + "/updateFeatures");
                var defaultProxy = new WebProxy { UseDefaultCredentials = true };
                client.Proxy = defaultProxy;

                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddParameter("features", updateFeatureRequest);
                //request.AddObject(updateFeatureRequest);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                if (token != null)
                    request.AddQueryParameter("token", token);
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                    throw new Exception("Esri Error Response Code: " + response.StatusCode);


                var content = response.Content;
                ret = JsonConvert.DeserializeObject<UpdateFeatureResult>(content);
                if (ret.error != null) throw new Exception(ret.error.message);
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Feature Service. Error Code : #-1002#", e);
            }

            return ret;
        }

        internal static DeleteFeatureResult DeleteFeatureTask(string featureUrl,
            DeleteFeatureRequest deleteFeatureRequest, string token = null)
        {
            DeleteFeatureResult ret;
            try
            {
                var client = new RestClient(featureUrl + "/deleteFeatures");
                var defaultProxy = new WebProxy { UseDefaultCredentials = true };
                client.Proxy = defaultProxy;

                var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };
                request.AddParameter("features", deleteFeatureRequest);
                //request.AddObject(deleteFeatureRequest);
                request.AddHeader("Content-Type", "application/json; charset=utf-8");
                if (token != null)
                    request.AddQueryParameter("token", token);
                var response = client.Execute(request);

                if (!response.IsSuccessful)
                    throw new Exception("Esri Error Response Code: " + response.StatusCode);


                var content = response.Content;
                ret = JsonConvert.DeserializeObject<DeleteFeatureResult>(content);
                if (ret.error != null) throw new Exception(ret.error.message);
            }
            catch (Exception e)
            {
                throw new Exception("Error in Esri Feature Service. Error Code : #-1002#", e);
            }

            return ret;
        }
    }
}