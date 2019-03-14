using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;

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
        public void Post([FromBody] BusinessLogic.Models.Comment comment)
        {
            _commentsService.AddComment(comment);
        }


        // POST api/values
        [HttpPost("comments/{commentId}")]
        public void Post([FromBody] BusinessLogic.Models.Comment comment, long commentId)
        {
            comment.ParentCommentInfo.Id = commentId;
            _commentsService.AddComment(comment);
        }

        // PUT api/values/5
        [HttpPut("comments/{commentId}")]
        public void Put([FromBody] BusinessLogic.Models.Comment comment)
        {
            _commentsService.EditComment(comment);
        }

        // DELETE api/values/5
        [HttpDelete("comments/{commentId}")]
        public void Delete(long commentId)
        {
            _commentsService.DeleteComment(commentId);
        }
    }
}
