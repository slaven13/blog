using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IPostsService _postService;

        public BlogController(IPostsService postService)
        {
            _postService = postService;
        }

        // GET api/values
        [HttpGet("posts")]
        public ActionResult<BusinessLogic.Models.StartPageModel> Get()
        {
            return _postService.GetStartPage();
        }

        // GET api/values/5
        [HttpGet("posts/{postId}")]
        public ActionResult<BusinessLogic.Models.Post> Get(long postId)
        {
            return _postService.GetPost(postId);
        }

        // POST api/values
        [HttpPost("posts")]
        public void Post([FromBody] BusinessLogic.Models.Post post)
        {
            _postService.AddPost(post);
        }

        // PUT api/values/5
        [HttpPut("posts/{postId}")]
        public void Put(long postId, [FromBody] BusinessLogic.Models.Post post)
        {
            post.Id = postId;
            _postService.EditPost(post);
        }

        // DELETE api/values/5
        [HttpDelete("posts/{postId}")]
        public void Delete(long postId)
        {
            _postService.DeletePost(postId);
        }
    }
}
