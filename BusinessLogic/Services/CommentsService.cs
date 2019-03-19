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
using AutoMapper;

namespace BusinessLogic.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<DataBaseAccessLayer.Data.Entities.Comment> _repository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentsService(IRepository<DataBaseAccessLayer.Data.Entities.Comment> repository,
                            ICommentRepository commentRepository,
                            IPostRepository postRepository,
                            IUserRepository userRepository,
                            IMapper mapper)
        {
            _repository = repository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }        

        public List<BusinessLogic.Models.Comment> GetPostComments(long postId)
        {
            return _mapper.Map<List<BusinessLogic.Models.Comment>>(_commentRepository.GetComments()
                                                                                     .Where(c => c.PostId == postId));
        }

        public BusinessLogic.Models.Comment GetComment(long commentId)
        {
            var comment = _commentRepository.GetFullComment(commentId);

            return _mapper.Map<BusinessLogic.Models.Comment>(comment);
        }

        public void AddComment(BusinessLogic.Models.Comment comment)
        {            
            _commentRepository.Add(_mapper.Map<DataBaseAccessLayer.Data.Entities.Comment>(comment));
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
                    var commentToDelete = _commentRepository.GetComments()
                                                            .FirstOrDefault(c => c.Id == commentId);

                    recursiveDelete(commentToDelete);
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
                    _repository.Update(_mapper.Map<DataBaseAccessLayer.Data.Entities.Comment>(comment));
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

        private void recursiveDelete(DataBaseAccessLayer.Data.Entities.Comment parent)
        {
            if (parent.Replies != null)
            {
                var replies = _commentRepository.GetComments()
                                                .Where(c => c.ParentCommentId == parent.Id);

                foreach(var reply in replies)
                {
                    recursiveDelete(reply);
                }                
            }

            _repository.Delete(parent);
        }
    }
}
