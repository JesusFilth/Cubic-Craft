using Source.Scripts.Core.Storage.Models;

namespace Source.Scripts.Core.Storage.User
{
    public interface IUserStorage
    {
        void SetUser(UserModel user);
    }
}