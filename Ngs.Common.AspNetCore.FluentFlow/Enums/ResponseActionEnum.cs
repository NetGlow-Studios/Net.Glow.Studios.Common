namespace Ngs.Common.AspNetCore.FluentFlow.Enums;

public enum ResponseActionEnum
{
    None = 0,
    RedirectToAction = 1,
    Redirect = 2,
    Modal = 3,
    Refresh = 4,
    Close = 5,
    HandleError = 6
}