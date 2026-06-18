using MangaWorkflow.Entities.HuyNQ.Models;
using MangaWorkflow.Repositories.HuyNQ.Base;
using MangaWorkflow.Repositories.HuyNQ.DBContext;

namespace MangaWorkflow.Repositories.HuyNQ;

public class ChapterMetaHuyNqRepository : GenericRepository<ChapterMetaHuyNq>
{
    public ChapterMetaHuyNqRepository()
    {
    }

    public ChapterMetaHuyNqRepository(MangaWorkflowContext context) => _context = context;
}
