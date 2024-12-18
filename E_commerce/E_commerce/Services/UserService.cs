using E_commerce.Interface;
using E_commerce.Models;

namespace E_commerce.Services
{
    public class UserService:IUserService
    {
        private IDatabaseService<User> _databaseService;
        public UserService(IDatabaseService<User> databaseService)
        {
            _databaseService = databaseService;
            _databaseService.SetCollection(nameof(User));
        }

        public async Task<List<User>> GetAsync()
        {
            return await _databaseService.GetAllAsync();
        }

        public async Task RegisterUserAsync(User user)
        {
            await _databaseService.AddAsync(user);
        }

        public async Task UnRegisterUserAsync(User user) {
            await _databaseService.DeleteAsync(user.Id);
        }


    }
}
