using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.Models;
using System.Text.RegularExpressions;
using System.Linq.Dynamic;

namespace BusinessLogic.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<DataBaseAccessLayer.Data.Entities.Comment> _repository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public CommentsService(IRepository<DataBaseAccessLayer.Data.Entities.Comment> repository,
                            ICommentRepository commentRepository,
                            IPostRepository postRepository)
        {
            _repository = repository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public List<BusinessLogic.Models.Comment> GetPostComments(long postId)
        {
            return _commentRepository.GetCommentsByPost(postId)
                                     .Select(c => new BusinessLogic.Models.Comment
                                                  {
                                                      Id = c.Id,
                                                      Content = c.Content,
                                                      CreationDate = c.CreationDate,
                                                      User = new BusinessLogic.Models.User
                                                      {
                                                          Id = c.UserId,
                                                          Username = c.User.Username
                                                      }
                                                  }            
                                     ).ToList();
        }

        public BusinessLogic.Models.Comment GetComment(long commentId)
        {
            var comment = _commentRepository.Get(commentId);
            var post = _postRepository.Get(comment.PostId);

            return new BusinessLogic.Models.Comment
            {
                Id = comment.Id,
                Content = comment.Content,
                CreationDate = comment.CreationDate,
                User = new BusinessLogic.Models.User
                       {
                            Id = comment.UserId,
                            Username = comment.User.Username
                       },
                Post = new BusinessLogic.Models.Post
                {
                    Id = comment.PostId,
                    Title = comment.Post.Title,
                    Content = comment.Post.Content.Length > Constants.PostPreviewMaxChars ?
                                                            ContentOperations.RemoveHtmlTags(comment.Post.Content).Substring(0, Constants.PostPreviewMaxChars) + Constants.ContentPreviewAppendix
                                                            : comment.Post.Content,
                }
            };         
        }

        public void AddComment(BusinessLogic.Models.Comment comment)
        {
            _commentRepository.Add(new DataBaseAccessLayer.Data.Entities.Comment
                                  {
                                        Content = comment.Content,
                                        CreationDate = DateTime.UtcNow,
                                        UserId = comment.UserId,
                                        PostId = comment.PostId,
                                        ParentCommentId = comment.ParentCommentId
                                  }
            );
        }

        public void DeleteComment(long commentId)
        {
            if (!commentExists(commentId))
            {
                throw new Exception("There is no comment with Id = ${commentId}");
            }
            else
            {
                try
                {
                    _repository.Delete(new DataBaseAccessLayer.Data.Entities.Comment
                                       {
                                           Id = commentId
                                       }
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void EditComment(BusinessLogic.Models.Comment comment)
        {
            if (!commentExists(comment.Id))
            {
                throw new Exception("There is no comment with Id = ${comment.Id}");
            }
            else
            {
                try
                {
                    _repository.Update(new DataBaseAccessLayer.Data.Entities.Comment
                                       {
                                           Id = comment.Id,
                                           Content = comment.Content
                                       }
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool commentExists(long commentId)
        {
            return _repository.Get().Any(c => c.Id == commentId);
        }        
    }
}
