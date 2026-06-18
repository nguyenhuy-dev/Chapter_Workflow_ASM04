using MangaWorkflow.Entities.HuyNQ.Models;
using MangaWorkflow.Repositories.HuyNQ.Base;
using MangaWorkflow.Repositories.HuyNQ.DBContext;
using Microsoft.EntityFrameworkCore;

namespace MangaWorkflow.Repositories.HuyNQ;

public class ChapterHuyNqRepository : GenericRepository<ChapterHuyNq>
{
    public ChapterHuyNqRepository()
    {
        
    }

    public ChapterHuyNqRepository(MangaWorkflowContext context) => _context = context;

    public new async Task<List<ChapterHuyNq>> GetAllAsync()
    {
        return await _context.ChapterHuyNqs
            .Include(x => x.ChapterMetaHuynq)
            .ToListAsync();
    }

    public new async Task<ChapterHuyNq?> GetByIdAsync(int id)
    {
        return await _context.ChapterHuyNqs
            .Include(x => x.ChapterMetaHuynq)
            .FirstOrDefaultAsync(x => x.HuynqId == id);
    }

    public async Task<List<ChapterHuyNq>> SearchAsync(string? title, int? chapterNumber, bool? approved)
    {
        return await _context.ChapterHuyNqs
            .Include(x => x.ChapterMetaHuynq)
            .Where(x =>
                (string.IsNullOrEmpty(title) || x.Title.Contains(title)) &&
                (chapterNumber == null || chapterNumber == x.ChapterNumber) &&
                (approved == null || approved == x.Approved)
            )
            .ToListAsync();
    }
}
