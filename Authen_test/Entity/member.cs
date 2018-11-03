namespace Authen_test.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("member")]
    public partial class member
    {
        [Key]
        public int mem_id { get; set; }

        [Required]
        [StringLength(100)]
        public string mem_usename { get; set; }

        [Required]
        public string mem_password { get; set; }

        [StringLength(100)]
        public string mem_f_name { get; set; }

        [StringLength(100)]
        public string mem_l_name { get; set; }
    }
}
