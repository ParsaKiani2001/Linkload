using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ResponceMessage
    {
        [Description("انجام شد")]
        Ok = 0,
        [Description("خطا داخلی")]
        ServerError = 500,
        [Description("لاگین انجام نشده")]
        UnAuthorized = 401,
        [Description("شما به این بخش دسترسی ندارید")]
        AccessDenied = 403,
        [Description("پارامتر های ارسالی نامعتبر است")]
        NoValidParametes = 1,
        [Description("نام کاربری یا کلمه عبور اشتباه است")]
        WrongUserPass = 2,
        [Description("کاربر فعال نمی باشد")]
        UserNotActive = 3,
        [Description("کاربر وجود دارد")]
        ExistUser = 4,
        [Description("لیست مورد نظر خالی می باشد")]
        Empty = 400,
        [Description("دیتای مورد نظر وجود ندارد")]
        NotFound = 404,
        [Description("طول متن {PropertyName} باید {MaxLength} باشد")]
        Length = 5,
        [Description("طول متن {PropertyName} باید از {MaxLength} بیشتر باشد")]
        MaxLength = 6,
        [Description("طول متن {PropertyName} باید از {MinLength} کمتر باشد")]
        MinLength = 7,
        [Description(" کد یکبار مصرف اشتباه است یا منقضی شده است")]
        OtpWrong = 9,
        [Description("حجم فایل مورد نظر بیش از حد مجاز است")]
        FileSizeLimit = 10,
        [Description("کاربر یافت نشد")]
        UserNotFound = 11,
    }
}
