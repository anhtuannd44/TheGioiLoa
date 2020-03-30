using PagedList;
using System.Collections.Generic;
using System.Linq;
using TheGioiLoa.Helper;
using TheGioiLoa.Models;


namespace TheGioiLoa.Service
{

    public class AccountService
    {
        private readonly TheGioiLoaModel db = new TheGioiLoaModel();
        private readonly HelperFunction _helper = new HelperFunction();
        private readonly ApplicationDbContext dbApp = new ApplicationDbContext();
        public string GetPartialView(string url)
        {
            var result = "";
            var partial = PartialList.Find(a => a.Url == url).PartialView;
            if (!string.IsNullOrEmpty(partial))
                result = partial;
            return result;
        }

        public dynamic GetModelPartial(string partial, string userId)
        {
            dynamic model;
            switch (partial)
            {
                case "_AccountManagePartial":
                    var user = dbApp.Users.Find(userId);
                    model = new UserInformationViewModel()
                    {
                        Address = user.Address,
                        UserId = user.Id,
                        BirthDay = user.Birthday,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                    };
                    break;
                case "_ChangePasswordPartial":
                    model = new ChangePasswordViewModel();
                    break;
                case "_OrderManagePartial":
                    model = db.Order.Where(a => a.UserId == userId).OrderByDescending(a => a.DateCreated).ToPagedList(1, 10);
                    break;
                default:
                    model = "";
                    break;
            }
            return model;
        }

        readonly List<AccountPartial> PartialList = new List<AccountPartial>()
            {
                new AccountPartial("quan-ly-don-hang","_OrderManagePartial"),
                new AccountPartial("quan-ly-thong-tin","_AccountManagePartial"),
                new AccountPartial("doi-mat-khau","_ChangePasswordPartial"),
                new AccountPartial("nhan-xet-cua-toi","_MyCommentPartial"),
                new AccountPartial("thong-tin-bao-hanh","_GuaranteePartial"),
            };


    }
}