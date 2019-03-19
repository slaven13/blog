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
    public class UsersService : IUsersService
    {
        private readonly IRepository<DataBaseAccessLayer.Data.Entities.User> _repository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersService(IRepository<DataBaseAccessLayer.Data.Entities.User> repository,
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

        public List<BusinessLogic.Models.User> GetUsers()
        {
            return _mapper.Map<List<BusinessLogic.Models.User>>(_userRepository.GetUsersWithPostsAndComments());
        }

        public BusinessLogic.Models.User GetUserFull(long userId)
        {
            return _mapper.Map<BusinessLogic.Models.User>(_userRepository.Get(userId));
        }

        public void AddUser(BusinessLogic.Models.User user)
        {
            _userRepository.Add(_mapper.Map<DataBaseAccessLayer.Data.Entities.User>(user));
        }

        public void DeleteUser(long userId)
        {
            if (!userExists(userId))
            {
                throw new Exception("There is no user with Id = ${userId}");
            }
            else
            {
                try
                {
                    _repository.Delete(new DataBaseAccessLayer.Data.Entities.User
                                       {
                                           Id = userId
                                       }
                    );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void EditUser(BusinessLogic.Models.User user)
        {
            if (!userExists(user.Id))
            {
                throw new Exception("There is no user with Id = ${user.Id}");
            }
            else
            {
                try
                {
                    _repository.Update(_mapper.Map<DataBaseAccessLayer.Data.Entities.User>(user));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool userExists(long userId)
        {
            return _userRepository.Get().Any(c => c.Id == userId);
        }
    }
}
