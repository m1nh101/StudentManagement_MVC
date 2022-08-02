using Application.Entities;
using Application.Interfaces;

namespace Application.Services;

public interface IRoleService
{
  Task CreateNew(string name);
  Task<IEnumerable<Tuple<string, string>>> GetAll();
}

public class RoleService : IRoleService
{
  private readonly IApplicationContext _context;

  public RoleService(IApplicationContext context)
  {
    _context = context;
  }

  public async Task CreateNew(string name)
  {
    var role = new Role() { Name = name };
    await _context.Roles.AddAsync(role);
    await _context.Save();
  }

  public Task<IEnumerable<Tuple<string, string>>> GetAll()
  {
    var query = _context.Roles
      .Select(e => new Tuple<string, string>(e.Id, e.Name))
      .AsEnumerable();
    
    return Task.FromResult(query);
  }
}