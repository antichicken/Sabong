namespace Sabong.Business
{
    public enum LoginResult
    {
        Successful = 1,
        WrongUserNameOrPassword = 2,
        Expired = 3,
        NotExist = 4,
        NotAllowAccessPlayerSite=5,
        Closed, // khong cho phep login
        Suspended,// co the view data nhung ko edit duoc data
    }
}