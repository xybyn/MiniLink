using Microsoft.AspNetCore.Mvc;
using MiniLink.App.Services;
using MiniLink.Persistance.SQLite;
using MiniLink.Web.Models;

namespace MiniLink.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinifyController : ControllerBase
    {
        private readonly MinifyLinkService _service;
        private readonly ApplicationDbContext _db;
        public MinifyController(MinifyLinkService service, ApplicationDbContext db)
        {
            _service = service;
            _db = db;
        }
        [HttpPost]
        public string Post(PostDataViewModel data)
        {
            return _service.Minify(data.Link);
        }
    }
}
