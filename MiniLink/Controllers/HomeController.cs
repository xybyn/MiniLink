using Microsoft.AspNetCore.Mvc;
using MiniLink.App.Services;
using MiniLink.Web.Models;

namespace MiniLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly MinifyLinkService _minifyLinkService;

        public HomeController(MinifyLinkService minifyLinkService)
        {
            _minifyLinkService = minifyLinkService;
        }

        public IActionResult Index(string link)
        {
            if (link != null)
            {
                return Redirect(_minifyLinkService.GetLong(link));
            }
            return View();
        }
        public IActionResult Minify(LinkViewModel viewModel)
        {
            ViewData["Link"] = _minifyLinkService.Minify(viewModel.Link);
            return View();
        }
    }
}
