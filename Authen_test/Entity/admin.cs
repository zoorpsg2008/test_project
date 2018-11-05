namespace Authen_test.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin")]
    public partial class admin
    {
        [Key]
        public int ad_id { get; set; }

        [StringLength(100)]
        public string ad_username { get; set; }

        public string ad_password { get; set; }

        [StringLength(100)]
        public string ad_f_name { get; set; }

        [StringLength(100)]
        public string ad_l_name { get; set; }

        public string roles { get; set; }
    }
}
