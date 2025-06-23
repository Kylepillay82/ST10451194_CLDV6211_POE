using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SearchController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new Search
        {
            EventTypes = _context.EventTypes.ToList(),
            Results = new List<Event>()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Index(Search model)
    {
        model.EventTypes = _context.EventTypes.ToList();

        var query = _context.Events
            .Include(e => e.Venue)
            .Include(e => e.EventType)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(model.EventName))
        {
            query = query.Where(e => e.EventName.Contains(model.EventName));
        }

        if (model.EventTypeId.HasValue)
        {
            query = query.Where(e => e.EventTypeId == model.EventTypeId);
        }

        if (model.StartDate.HasValue)
        {
            query = query.Where(e => e.EventDate >= model.StartDate.Value);
        }

        if (model.EndDate.HasValue)
        {
            query = query.Where(e => e.EventDate <= model.EndDate.Value);
        }

        if (!string.IsNullOrWhiteSpace(model.VenueName))
        {
            query = query.Where(e => e.Venue != null && e.Venue.VenueName.Contains(model.VenueName));
        }

        if (model.VenueAvailable.HasValue)
        {
            // Assuming Venue has an "IsAvailable" property
            query = query.Where(e => e.Venue != null && e.Venue.IsAvailable == model.VenueAvailable.Value);
        }

        model.Results = await query.ToListAsync();

        return View(model);
    }
}
