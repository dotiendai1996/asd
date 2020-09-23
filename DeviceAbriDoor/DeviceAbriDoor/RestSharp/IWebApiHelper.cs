using DeviceAbriDoor.Models;

namespace DeviceAbriDoor.RestSharp
{
    public interface IWebApiHelper
    {
        void SetToken(string token);
        ResultModel Get(string url, params ParameterModel[] parameter);
        ResultModel GetList(string url, int pageIndex, params ParameterModel[] parameter);
        ResultModel GetForEdit(string url, object id);
        ResultModel Post(string url, object data);
        ResultModel Put(string url, object data);
        ResultModel Delete(string url, object id);
        void CheckWebApi();
    }
}
