using Abp.Domain.Entities;

namespace Hinnova.EntityFrameworkCore.StoreProcedure
{
    public class GetProcDetail : Entity<long>
    {
        public string PACKAGE_NAME { get; set; }
        public string OBJECT_NAME { get; set; }
        public string ARGUMENT_NAME { get; set; }
        public int POSITION { get; set; } //0 IS RETURN VALUE
        public string DATA_TYPE { get; set; }
        public string DEFAULT_VALUE { get; set; }
        public string IN_OUT { get; set; }
        public int MAX_LENGTH { get; set; }
    }
}
