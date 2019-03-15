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
    public class PostsService : IPostsService
    {
        private readonly IRepository<DataBaseAccessLayer.Data.Entities.Post> _repository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostsService(IRepository<DataBaseAccessLayer.Data.Entities.Post> repository,
                            ICommentRepository commentRepository,
                            IPostRepository postRepository,
                            IUserRepository userRepository,                            
                            IMapper mapper
                            )
        {
            _repository = repository;
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public StartPageModel GetStartPage()
        {
            var mostRecentPosts = getMostRecentPosts(Constants.TopNMostRecentPosts);

            var mostCommentedPosts = getMostCommentedPosts(Constants.TopNMostCommentedPosts);
            
            var mostRecentComments = getPostMostRecentComments(Constants.AllPosts, Constants.TopNMostRecentComments);            

            var mostActiveUsers = getTopNActiveUsers(Constants.TopNMostActiveUsers);

            return new StartPageModel
            {
                MostRecentPosts = mostRecentPosts,
                MostCommentedPosts = mostCommentedPosts,
                MostRecentCommments = mostRecentComments,                
                MostActiveUsers = mostActiveUsers
            };
        }

        public BusinessLogic.Models.Post GetPost(long postId)
        {
            return _mapper.Map<BusinessLogic.Models.Post>(_postRepository.GetPostFull(postId));
            //var commentsByPost = _commentRepository.GetCommentsByPost(postId);

            //return new BusinessLogic.Models.Post
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    Content = post.Title,
            //    UserInfo = _mapper.Map<BusinessLogic.Models.UserInfo>(post.User),
            //    CreationDate = post.CreationDate,
            //    Comments = _mapper.Map<List<BusinessLogic.Models.Comment>>(post.Comments)
            //};
        }

        public void AddPost(BusinessLogic.Models.Post post)
        {
            try
            {
                _repository.Add(_mapper.Map<DataBaseAccessLayer.Data.Entities.Post>(post));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeletePost(long postId)
        {
            if (!postExists(postId))
            {
                throw new Exception("There is no post with Id = ${postId}");
            }
            else
            {
                try
                {
                    _repository.Delete(new DataBaseAccessLayer.Data.Entities.Post
                                       {
                                           Id = postId
                                       }
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }            
        }

        public void EditPost(BusinessLogic.Models.Post post)
        {
            if (!postExists((long)post.Id))
            {
                throw new Exception("There is no post with Id = ${post.Id}");
            }
            else
            {
                try
                {
                    _repository.Update(_mapper.Map<DataBaseAccessLayer.Data.Entities.Post>(post));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private bool postExists(long postId)
        {
            return _repository.Get().Any(p => p.Id == postId);
        }

        private List<PostPreview> getMostCommentedPosts(int n)
        {
            var postsWithComments = _mapper.Map<List<PostPreview>>(_postRepository.GetPostsWithComments());

            foreach (var post in postsWithComments)
            {
                post.CommentsPreview = post.CommentsPreview.OrderByDescending(c => c.CreationDate)
                                                           .Take(Constants.TopNMostRecentComments)
                                                           .ToList();
            }

            return _mapper.Map<List<PostPreview>>(postsWithComments.OrderByDescending(p => p.CommentsPreview.Count()).Take(n));
        }

        private List<CommentPreview> getPostMostRecentComments(long postId, int n)
        {
            var comments = postId <= 0 ? _commentRepository.Get().ToList() : _commentRepository.GetCommentsByPost(postId).ToList();

            return _mapper.Map<List<CommentPreview>>(comments.OrderByDescending(c => c.CreationDate).Take(n));
        }

        private List<PostPreview> getMostRecentPosts(int n)
        {
            var mostRecentPosts = _mapper.Map<IEnumerable<PostPreview>>(_postRepository.GetPostsWithComments()
                                                                                .OrderByDescending(p => p.CreationDate)
                                                                                .Take(n).ToList());
            foreach(var post in mostRecentPosts)
            {
                post.CommentsPreview = post.CommentsPreview.OrderByDescending(c => c.CreationDate)
                                                           .Take(Constants.TopNMostRecentComments)
                                                           .ToList();
            }

            return mostRecentPosts.ToList();
        }

        private List<UserInfo> getTopNActiveUsers(int n)
        {
            var usersWithMostComments = _commentRepository.Get()
                                                          .GroupBy(c => c.User)
                                                          .Select(g => new
                                                          {
                                                              User = g.Key,
                                                              Count = g.Count()
                                                          })
                                                          .OrderByDescending(x => x.Count)
                                                          .ToList();

            var usersWithMostPosts = _repository.Get()
                                                .GroupBy(p => p.User)
                                                .Select(g => new
                                                {
                                                    User = g.Key,
                                                    Count = g.Count()                                       
                                                })
                                                .OrderByDescending(x => x.Count)
                                                .ToList();

            List<UserActivities> usersCounts = new List<UserActivities>();
            foreach(var postUser in usersWithMostPosts)
            {
                var totalCountActivities = postUser.Count;
                foreach(var commentUser in usersWithMostComments)
                {
                    if (postUser.User.Id == commentUser.User.Id)
                    {
                        totalCountActivities = totalCountActivities + commentUser.Count;
                    }
                }

                usersCounts.Add(new UserActivities
                                {
                                    Id = postUser.User.Id,
                                    Username = postUser.User.Username,
                                    Count = totalCountActivities
                                }
                );
            }

            foreach (var commentUser in usersWithMostComments)
            {
                var totalCountActivities = commentUser.Count;
                foreach (var postUser in usersWithMostPosts)
                {
                    if (commentUser.User.Id == postUser.User.Id)
                    {
                        totalCountActivities = totalCountActivities + postUser.Count;
                    }
                }

                if (!usersCounts.Any(u => u.Id == commentUser.User.Id))
                {
                    usersCounts.Add(new UserActivities
                                    {
                                        Id = commentUser.User.Id,
                                        Username = commentUser.User.Username,
                                        Count = totalCountActivities
                                    }
                    );
                }                
            }

            return usersCounts.OrderByDescending(u => u.Count)
                              .Take(n)
                              .Select(u => _mapper.Map<BusinessLogic.Models.UserInfo>(u))
                              .ToList();
        }

    }
}
