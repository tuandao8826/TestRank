using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestRank.Entities;

namespace TestRank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RanksController : ControllerBase
    {
        private readonly RankContext _context;

        public RanksController(RankContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetRankByTime([FromBody] GetRanking request)
        {
            // Giả xử user có id = 5 đăng nhập
            var userId = 5;

            // Lấy tổng số trang
            var totalCount = _context.Scores
                .GroupBy(item => new { item.UserId, item.PlayDay.Month, item.PlayDay.Year })
                .Count();

            var ranking = _context.Scores
                .Where(item => item.PlayDay.Month == request.Month && item.PlayDay.Year == request.Year)
                .Join(_context.Users,
                    score => score.UserId,
                    user => user.Id,
                    (score, user) => new { score, user })
                .GroupBy(
                    item => new { item.score.UserId, item.user.Name, item.user.Image, item.score.PlayDay.Month, item.score.PlayDay.Year },
                    item => item.score)
                .Select(g => new RankDTO
                {
                    UserId = g.Key.UserId,
                    Name = g.Key.Name,
                    Image = g.Key.Image != null ? g.Key.Image : "",
                    Scores = g.Sum(s => s.Point),
                })
                .OrderByDescending(item => item.Scores)
                .Skip((request.PageNumber - 1) * request.PageSize) 
                .Take(request.PageSize) 
                .ToList();

            var myRanking = ranking.Where(item => item.UserId == userId);

            return Ok(new
            {
                StatusCode = 200,
                Messages = string.Empty,
                Obj = new
                {
                    TotalCount = totalCount,
                    PageSize = request.PageSize,
                    CurrentPage = request.PageNumber,
                    Items = new
                    {
                        MyRanking = myRanking,
                        Ranking = ranking,
                    },
                }
            });
        }
    }
}
