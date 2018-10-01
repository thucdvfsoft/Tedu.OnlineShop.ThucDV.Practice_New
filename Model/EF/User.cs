namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name="Tài khoản")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name="Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name="Tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name="Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name="Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name="Điện thoại")]
        public string Phone { get; set; }

        [Display(Name="Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name="Tạo bởi")]
        public string CreateBy { get; set; }

        [Display(Name="Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [MaxLength(50)]
        [Display(Name="Sửa bởi")]
        public string ModifiedBy { get; set; }

        [Display(Name="Trạng thái")]
        public bool Status { get; set; }
    }
}
