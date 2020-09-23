using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceAbriDoor.Models
{
    public class ParameterModel
    {
        public ParameterModel() { }

        public ParameterModel(string fieldName, object value)
        {
            FieldName = fieldName;
            Value = value;
        }

        public string FieldName { get; set; }
        public object Value { get; set; }
    }
}
