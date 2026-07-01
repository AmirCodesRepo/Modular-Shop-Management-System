namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void Signin(AuthViewModel authViewModel);
        void SignOut();
        bool IsAuthenticated();
        string CurrentUserRole();
        void getUserInfo();
    }
}
