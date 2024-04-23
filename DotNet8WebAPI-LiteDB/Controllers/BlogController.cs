using DotNet8WebAPI_LiteDB.Model.Blog;
using LiteDB;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebAPI_LiteDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly string _filePath;
        private readonly string _folderPath;

        public BlogController()
        {
            _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDB");
            Directory.CreateDirectory(_folderPath);

            _filePath = Path.Combine(_folderPath, "Blog.db");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var lst = collection.FindAll().ToList();
            db.Dispose();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id) {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var item = collection.Find(x => x.BlogId == id).FirstOrDefault();
            db.Dispose();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var db = new LiteDatabase(_filePath);
            var collection = db.GetCollection<BlogModel>("Blog");
            var createBlog = new BlogModel
            {
                BlogId = Ulid.NewUlid().ToString(),
                BlogTitle = "TestListDB",
                BlogAuthor = "TestListDB",
                BlogContent = "TestListDB",
            };
            collection.Insert(createBlog);
            db.Dispose();
            return Ok(createBlog);
        }
    }
}
