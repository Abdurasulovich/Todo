using To_Do.ViewModels.Models;

namespace To_Do.Services.Interfaces;

public interface ITodoService
{
    public Task<List<TodoViewModel>> GetAll();

    public Task<TodoViewModel> GetByIdAsync(long Id);

    public Task<int> SaveAsync(TodoViewModel model);

    public Task<bool> ChangeTodoCompletedOrIncompleted(long id, bool isCompleted);
    public Task<int> UpdateAsync(TodoViewModel model);

    public Task<int> DeleteAsync(TodoViewModel model);
}
