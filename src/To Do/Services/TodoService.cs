using To_Do.DataAccess;
using To_Do.DataAccess.Models;
using To_Do.Services.Interfaces;
using To_Do.ViewModels.Models;

namespace To_Do.Services;

public class TodoService(DataContext dbContext) : ITodoService
{
    private DataContext _dbContext = dbContext;
    public async Task<List<TodoViewModel>> GetAll()
    {
        try
        {
            var values = await (await _dbContext.GetAll<TodoModel>()).ToListAsync();
            var todoIds = values.Select(t=>t.Id).ToList();

            var result = values.Select(todo => new TodoViewModel
            {
                
                Id = todo.Id,
                Name = todo.Name,
                AddNote = todo.AddNote,
                DueDate = todo.DueDate,
                IsImportant = todo.IsImportant,
                IsDone = todo.IsDone,
                UpdatedDate = todo.UpdatedDate,
                CreatedDate = todo.CreatedDate,

            }).ToList();
            return result;
        }catch (Exception ex)
        {
            return new List<TodoViewModel>();
        }
    }

    public async Task<TodoViewModel> GetByIdAsync(long Id)
    {
        try
        {
            var value = await _dbContext.GetById<TodoModel>(Id);

            return new TodoViewModel
            {
                Id = value.Id,
                Name = value.Name,
                AddNote = value.AddNote,
                DueDate = value.DueDate,
                IsImportant = value.IsImportant,
                IsDone = value.IsDone,
                CreatedDate = value.CreatedDate,
                UpdatedDate = value.UpdatedDate,
            };
        }
        catch (Exception ex)
        {
            return new();
        }
    }

    public async Task<int> SaveAsync(TodoViewModel model)
    {
        try
        {
            var todoModel = new TodoModel
            {
                Id = model.Id,
                Name = model.Name,
                AddNote = model.AddNote,
                DueDate = model.DueDate,
                IsImportant = model.IsImportant,
                IsDone = model.IsDone,
                CreatedDate = DateTime.UtcNow.AddHours(5),
                UpdatedDate = DateTime.UtcNow.AddHours(5)
            };
            var todoResult = await _dbContext.Save<TodoModel>(todoModel);

            return todoResult;
        }catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<int> UpdateAsync(TodoViewModel model)
    {   
        try
        {
            var todoModel = new TodoModel
            {
                Id = model.Id,
                Name = model.Name,
                AddNote = model.AddNote,
                DueDate = model.DueDate,
                IsImportant = model.IsImportant,
                IsDone = model.IsDone,
                CreatedDate = model.CreatedDate,
                UpdatedDate = DateTime.Now
            };
            var todoResult = await _dbContext.Update<TodoModel>(todoModel);

            return todoResult;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public async Task<int> DeleteAsync(TodoViewModel model)
    {
        try
        {
            var result = new TodoModel
            {
                Id = model.Id,
                Name = model.Name,
                AddNote = model.AddNote,
                DueDate = model.DueDate,
                IsDone = model.IsDone,
                IsImportant = model.IsImportant,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };

            return await _dbContext.Delete<TodoModel>(result);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<bool> ChangeTodoCompletedOrIncompleted(long id, bool isCompleted)
    {
        var todo = await _dbContext.GetById<TodoModel>(id);
        if(todo is not null)
        {
            todo.IsDone = !isCompleted;
            var result = await _dbContext.Update(todo);
            return result > 0;
        }
        return false;
    }
}
