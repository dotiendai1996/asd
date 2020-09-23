using DeviceAbriDoor.Cachings;
using DeviceAbriDoor.Configs;
using DeviceAbriDoor.Exceptions;
using DeviceAbriDoor.Models;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DeviceAbriDoor.RestSharp
{
    public class WebApiHelper
    {
        private AppConfigSection configs;
        private RestClient client;

        private static WebApiHelper instance = new WebApiHelper();
        public static WebApiHelper Instance
        {
            get
            {
                if (instance == null) instance = new WebApiHelper();

                instance.SetToken(LocalStorageCaching.Instance.AdminSession?.AccessToken);

                return instance;
            }
        }

        public WebApiHelper()
        {
            configs = AppConfigSection.GetInstance();
            client = new RestClient(configs.WebApi.ServerAddress);
        }

        public bool CheckServerAddress()
        {
            if (Uri.IsWellFormedUriString(configs.WebApi.ServerAddress, UriKind.Absolute))
                return true;

            throw new Exception("Địa chỉ máy chủ không đúng. Vui lòng kiểm tra lại.");
        }

        public void SetBaseUrl(string url)
        {
            if (CheckServerAddress())
                client.BaseUrl = new Uri(url);
        }

        public void SetToken(string token)
        {
            client.RemoveDefaultParameter("Authorization");
            client.AddDefaultHeader("Authorization", $"Bearer {token}");
        }

        public ResultModel Get(string url, params ParameterModel[] parameter)
        {
            var request = new RestRequest($"{url}");
            foreach (var param in parameter)
            {
                if (param.Value != null)
                    request.AddParameter(param.FieldName, param.Value);
            }

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.Method = Method.GET;
            Task<IRestResponse<ResultModel>> response = client.ExecuteTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }

        public ResultModel GetList(string url, int pageIndex, params ParameterModel[] parameter)
        {
            var request = new RestRequest($"{url}");

            request.AddParameter("MaxResultCount", configs.WebApi.MaxResultCount.ToString());
            request.AddParameter("SkipCount", (pageIndex * AppConfigSection.GetInstance().WebApi.MaxResultCount).ToString());

            foreach (var param in parameter)
            {
                if (param.Value != null)
                    request.AddParameter(param.FieldName, param.Value);
            }

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.Method = Method.GET;
            Task<IRestResponse<ResultModel>> response = client.ExecuteGetTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }

        public ResultModel GetForEdit(string url, object id)
        {
            var request = new RestRequest($"{url}");
            request.AddParameter("Id", id);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.Method = Method.GET;
            Task<IRestResponse<ResultModel>> response = client.ExecuteTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }

        public ResultModel Post(string url, object data)
        {
            var request = new RestRequest($"{url}");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(data);

            request.Method = Method.POST;
            Task<IRestResponse<ResultModel>> response = client.ExecuteTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }

        public ResultModel Put(string url, object data)
        {
            var request = new RestRequest($"{url}");

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(data);

            request.Method = Method.PUT;
            Task<IRestResponse<ResultModel>> response = client.ExecuteTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }

        public ResultModel Delete(string url, object id)
        {
            var request = new RestRequest($"{url}");
            request.AddParameter("Id", id);
            request.AddHeader("Content-Type", "application/json");

            request.Method = Method.DELETE;
            Task<IRestResponse<ResultModel>> response = client.ExecuteTaskAsync<ResultModel>(request);

            response.Wait();

            if (response.Result.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.Result.Data.Success)
                    return response.Result.Data;
                else
                    throw new HandleException(response.Result.Data.Error);
            }
            else
            {
                throw new HandleException(0, response.Result.Content.ToString());
            }
        }
    }
}
