using DeviceAbriDoor.Models;
using DeviceAbriDoor.Utils;
using Newtonsoft.Json;
using System;

namespace DeviceAbriDoor.Exceptions
{
    public class HandleException : Exception
    {
        public int Code;
        public string Detail;
        public ErrorModel errorModel;

        private HandleException() : base() { }

        private HandleException(string msg) : base(msg) { }

        public HandleException(ErrorModel _errorModel)
        {
            errorModel = _errorModel;
            LogUtils.WirteLogDebug($"Unexpected Error {JsonConvert.SerializeObject(_errorModel)}");
        }

        public HandleException(int code, string moreInformation)
        {
            Code = code;
            Detail = moreInformation;
        }

        public HandleException(int code) : this(code, string.Empty) { }

        /// <summary>
        /// Hiện thông báo lỗi của exception
        /// </summary>
        /// <param name="isIncludeInformation">
        /// Cho biết có hiện thông tin trường Detail hay không
        /// do trường này hiển thị trên một dòng riêng nên có
        /// thể không thích hợp với một số thành phần giao diện
        /// như label
        /// </param>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            if (errorModel == null)
                return Detail;

            if (!errorModel.Message.IsNullOrWhiteSpace())
                return $"{errorModel.Message} \n {errorModel.Details}";

            if (errorModel.ValidationErrors != null &&
                !errorModel.ValidationErrors.Message.IsNullOrWhiteSpace())
                return errorModel.ValidationErrors.Message;

            return Detail;
        }
    }
}
