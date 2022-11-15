using System;
using System.Collections.Generic;

namespace ProjectPRN.Models
{
    public partial class TblHoadon
    {
        public TblHoadon()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public decimal MaHd { get; set; }
        public string MaKh { get; set; } = null!;
        public DateTime NgayHd { get; set; }

        public virtual TblKhachHang MaKhNavigation { get; set; } = null!;
        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
