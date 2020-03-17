using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Route("api/blog/posts/{postId}")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET api/values
        [HttpGet("comments")]
        public ActionResult<List<BusinessLogic.Models.Comment>> Get(long postId)
        {
            return _commentsService.GetPostComments(postId);
        }

        // GET api/values/5
        [HttpGet("comments/{commentId}")]
        public ActionResult<BusinessLogic.Models.Comment> Get(long postId, long commentId)
        {
            return _commentsService.GetComment(commentId);
        }

        // POST api/values
        [HttpPost("comments")]
        [Authorize]
        public void Post(long postId, [FromBody] BusinessLogic.Models.Comment comment)
        {
            _commentsService.AddComment(comment);
        }


        // POST api/values
        [HttpPost("comments/{commentId}")]
        [Authorize]
        public void Post(long postId, long commentId, [FromBody] BusinessLogic.Models.Comment comment)
        {
            comment.ParentCommentId = commentId;
            _commentsService.AddComment(comment);
        }

        // PUT api/values/5
        [HttpPut("comments/{commentId}")]
        [Authorize]
        public void Put(long postId, long commentId, [FromBody] BusinessLogic.Models.Comment comment)
        {
            comment.Id = commentId;
            _commentsService.EditComment(comment);
        }

        // DELETE api/values/5
        [HttpDelete("comments/{commentId}")]
        [Authorize]
        public void Delete(long postId, long commentId)
        {
            _commentsService.DeleteComment(commentId);
        }
    }
}
