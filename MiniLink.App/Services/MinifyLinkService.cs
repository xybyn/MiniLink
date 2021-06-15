using MiniLink.Common;
using MiniLink.Domain.Models;
using MiniLink.Persistance.SQLite;
using System.Linq;

namespace MiniLink.App.Services
{
    public class MinifyLinkService
    {
        private readonly ApplicationDbContext _db;
        private readonly int _maxHashLen = 7;
        private readonly string _host = "https://localhost:44346/Home/Index/";

        public MinifyLinkService(ApplicationDbContext db)
        {
            _db = db;
        }

        public string Minify(string inputLink)
        {
            var hash = Utils.Sha256(inputLink);
            var sameLinks = from l in _db.Links where l.LongLink == inputLink select l;
            if (sameLinks.Any())
                return _host + sameLinks.First().ShortLink;
            var shorterHash = hash.Substring(0, _maxHashLen);
            _db.Links.Add(new Link
            {
                LongLink = inputLink,
                ShortLink = shorterHash
            });
            _db.SaveChanges();

            return _host + shorterHash;
        }

        public string GetLong(string hash)
        {
            var result = from link in _db.Links where link.ShortLink == hash select link;
            return result.First().LongLink;
        }
    }
}
