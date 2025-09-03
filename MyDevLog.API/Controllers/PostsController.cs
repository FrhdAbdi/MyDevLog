using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDevLog.API.Data;
using MyDevLog.API.Data.Entities;
using MyDevLog.API.DTOs;

namespace MyDevLog.API.Controllers;


[Route("api/Posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PostsController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostSummaryDTO>>> GetPosts()
    {
        var posts = await _context.Posts
            .OrderByDescending(p => p.PublishedDate)
            .Select(p => new PostSummaryDTO
            {
                Id = p.Id,
                Title = p.Title,
                Summary = p.Summary,
                PublishedDate = p.PublishedDate,
            })
            .ToListAsync();

        return Ok(posts);
    }


    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<PostSummaryDTO>>> GetLatestPosts()
    {
        var latestPosts = await _context.Posts
            .OrderByDescending(p => p.PublishedDate)
            .Take(4)
            .Select(p => new PostSummaryDTO
            {
                Id = p.Id,
                Title = p.Title,
                Summary = p.Summary,
                PublishedDate = p.PublishedDate
            })
            .ToListAsync();

        return Ok(latestPosts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _context.Posts.FindAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return post;
    }
}
