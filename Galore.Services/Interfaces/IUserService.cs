namespace Galore.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        int CreateUser(UserInputModel user);
        UserDetailDTO GetUserById(int userId);
        void DeleteUser(int userId);
        void UpdateUser(UserInputModel user, int userId);

    }
}