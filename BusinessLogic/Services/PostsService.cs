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
            foreach(var post in mostCommentedPosts)
            {
                post.Comments = getPostMostRecentComments(post.Id, Constants.TopNMostRecentCommentsByPost);
            }

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
            var post = _postRepository.Get(postId);
            var commentsByPost = _commentRepository.GetCommentsByPost(postId);

            return new BusinessLogic.Models.Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Title,
                User = new BusinessLogic.Models.User
                {
                    Id = post.User.Id,
                    Username = post.User.Username
                },
                CreationDate = post.CreationDate,
                Comments = commentsByPost.Select(c => new BusinessLogic.Models.Comment
                                                      {
                                                          Id = c.Id,
                                                          Content = c.Content
                                                      }
                                          )
                                         .ToList()
            };
        }

        public void AddPost(BusinessLogic.Models.Post post)
        {
            try
            {
                _repository.Add(new DataBaseAccessLayer.Data.Entities.Post
                                {
                                    Title = post.Title,
                                    Content = post.Content,
                                    UserId = post.User.Id,                                    
                                    CreationDate = DateTime.UtcNow
                                }
                );
            }
            catch (Exception ex)
            {
                throw ex;
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
            if (!postExists(post.Id))
            {
                throw new Exception("There is no post with Id = ${post.Id}");
            }
            else
            {
                try
                {
                    _repository.Update(new DataBaseAccessLayer.Data.Entities.Post
                                       {
                                            Id = post.Id,
                                            Title = post.Title,
                                            Content = post.Content
                                       }
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool postExists(long postId)
        {
            return _repository.Get().Any(p => p.Id == postId);
        }

        private List<BusinessLogic.Models.Post> getMostCommentedPosts(int n)
        {
            var postsWithComments = _postRepository.GetPostsWithComments();

            return postsWithComments
                              .OrderByDescending(p => p.Comments.Count())
                              .Take(n)
                              .Select(p => new BusinessLogic.Models.Post
                                           {
                                               Id = p.Id,
                                               Title = p.Title,                                               
                                               CreationDate = p.CreationDate,
                                               User = new BusinessLogic.Models.User
                                               {
                                                   Id = p.UserId,
                                                   Username = _userRepository.Get(p.UserId).Username
                                               },
                                               Comments = p.Comments
                                                           .OrderByDescending(c => c.CreationDate)
                                                           .Take(Constants.TopNMostRecentComments)
                                                           .Select(c => new BusinessLogic.Models.Comment
                                                                        {
                                                                            Id = c.Id,
                                                                            User = new BusinessLogic.Models.User
                                                                            {
                                                                                Id = c.User.Id,
                                                                                Username = c.User.Username
                                                                            },                                                               
                                                                            UserId = c.UserId,
                                                                            PostId = c.PostId,
                                                                            Content = c.Content,
                                                                            CreationDate = c.CreationDate
                                                                        }                                                           
                                                           ).ToList()
                                           }                       
                              )    
                              .ToList();
        }

        private List<BusinessLogic.Models.Comment> getPostMostRecentComments(long postId, int n)
        {
            List<DataBaseAccessLayer.Data.Entities.Comment> comments = null;
            
            if (postId <= 0)
            {
                 comments = _commentRepository.Get()
                                              .OrderByDescending(c => c.CreationDate)
                                              .Take(n)
                                              .ToList();
            }
            else
            {
                comments = _commentRepository.GetCommentsByPost(postId)
                                             .OrderByDescending(c => c.CreationDate)
                                             .Take(n)
                                             .ToList();
            }

            return comments.Select(c => new BusinessLogic.Models.Comment
                                        {
                                             Content = c.Content.Length > Constants.CommentPreviewMaxChars ? 
                                                       ContentOperations.RemoveHtmlTags(c.Content).Substring(0, Constants.CommentPreviewMaxChars) + Constants.ContentPreviewAppendix
                                                       : c.Content,
                                             User = new BusinessLogic.Models.User
                                             {
                                                 Id = c.UserId,
                                                 Username = _userRepository.Get(c.UserId).Username
                                             }
                                        }
                            )
                            .ToList();
        }

        private List<BusinessLogic.Models.Post> getMostRecentPosts(int n)
        {
            var mostRecentPosts = _mapper.Map<List<BusinessLogic.Models.Post>>(_postRepository.GetPostsWithComments()
                                                                                              .OrderByDescending(p => p.CreationDate)
                                                                                              .Take(n));
            foreach(var post in mostRecentPosts)
            {
                post.Comments = post.Comments.OrderByDescending(c => c.CreationDate)
                                             .Take(Constants.TopNMostRecentComments)
                                             .ToList();
            }

            return mostRecentPosts;

            //return _repository.Get()
            //                  .OrderByDescending(p => p.CreationDate)
            //                  .Take(n)
            //                  .Select(p => new BusinessLogic.Models.Post
            //                               {
            //                                   Id = p.Id,
            //                                   Title = p.Title,
            //                                   Content = p.Content.Length > Constants.PostPreviewMaxChars ?                                                
            //                                                                ContentOperations.RemoveHtmlTags(p.Content)
            //                                                                    .Substring(0, Constants.PostPreviewMaxChars)
            //                                                                    + Constants.ContentPreviewAppendix
            //                                                                : p.Content,
            //                                   Comments = _commentRepository.GetCommentsByPost(p.Id)
            //                                                                .Select(c => new BusinessLogic.Models.Comment
            //                                                                             {
            //                                                                                 Id = c.Id,
            //                                                                                 Content = c.Content.Length > Constants.CommentPreviewMaxChars ?
            //                                                                                                              ContentOperations.RemoveHtmlTags(c.Content)
            //                                                                                                                .Substring(0, Constants.CommentPreviewMaxChars)
            //                                                                                                                + Constants.ContentPreviewAppendix
            //                                                                                                              : c.Content

            //                                                                             }
            //                                                                )
            //                                                                .ToList(),
            //                                   User = new BusinessLogic.Models.User
            //                                   {
            //                                       Id = p.UserId,
            //                                       Username = _userRepository.Get(p.UserId).Username
            //                                   },
            //                                   CreationDate = p.CreationDate
            //                               }                                    
            //                  )
            //                  .ToList();
        }

        private List<BusinessLogic.Models.User> getTopNActiveUsers(int n)
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
                              .Select(u => new BusinessLogic.Models.User
                                           {
                                               Id = u.Id,
                                               Username = u.Username
                                           }
                              )
                              .ToList();
        }

    }
}
