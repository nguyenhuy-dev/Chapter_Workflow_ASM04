using MangaWorkflow.Entities.HuyNQ.Models;
using MangaWorkflow.Repositories.HuyNQ.Base;
using MangaWorkflow.Repositories.HuyNQ.DBContext;
using Microsoft.EntityFrameworkCore;

namespace MangaWorkflow.Repositories.HuyNQ;

public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
{
    public SystemUserAccountRepository()
    {
        
    }

    public SystemUserAccountRepository(MangaWorkflowContext context) => _context = context;

    public async Task<SystemUserAccount?> GetUserAccount(string userName, string password)
    {
        return await _context.SystemUserAccounts.FirstOrDefaultAsync(
            x => x.Email == userName && 
                x.Password == password &&
                x.IsActive
        );
    }
}
