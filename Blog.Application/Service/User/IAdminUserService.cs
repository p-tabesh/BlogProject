using Blog.Application.Model.User;

namespace Blog.Application.Service.User;

public interface IAdminUserService
{
    int CreateAdminUser(RegisterRequest request);
    void DeActiveUser(int userId);
    void ActiveUser(int userId);
    IEnumerable<Domain.Entity.User> GetAll();
}
