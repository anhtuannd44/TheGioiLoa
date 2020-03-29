using System.ComponentModel.DataAnnotations;

namespace TheGioiLoa.Enums
{
    public enum OrderStatusEnum
    {
        [Display(Name = "Đang xử lý")]
        Processing = 1,
        [Display(Name = "Hoàn tất")]
        Completed = 2,
        [Display(Name = "Đã xóa")]
        Removed = 3,
    }
}