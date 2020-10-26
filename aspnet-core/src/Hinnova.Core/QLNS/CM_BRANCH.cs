using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hinnova.QLNS
{
    [Table("CM_BRANCH")]
    public class CM_BRANCH : Entity<string>
    {
        [Column(TypeName ="varchar(15)")]
        public virtual string FATHER_ID { get; set; }
        [Column(TypeName = "varchar(15)")]
        public virtual string BRANCH_CODE { get; set; }
        [StringLength(200)]
        public virtual string BRANCH_NAME { get; set; }
        [Column(TypeName = "varchar(55)")]
        public virtual string BRANCH_TYPE { get; set; }
        [Column(TypeName = "varchar(50)")]
        public virtual string AUTH_STATUS { get; set; }


        [ForeignKey("FATHER_ID")]
        public virtual CM_BRANCH ProductCategory { get; set; }
    }
}
