using FlightAdvisor.Core.CustomExceptions;
using FlightAdvisor.Domain.Models;
using FlightAdvisor.Interfaces.Repositories;
using FlightAdvisor.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightAdvisor.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICityRepository _cityRepository;

        public CommentService(ICommentRepository commentRepository, ICityRepository cityRepository)
        {
            _commentRepository = commentRepository;
            _cityRepository = cityRepository;
        }

        public void Add(Comment comment)
        {
            if (!CheckIfCityExist(comment.CityId))
            {
                throw new NotFoundCityException();
            }
            else
            {
                _commentRepository.Add(comment);
            }
        }

        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public Comment Get(int id)
        {
           return _commentRepository.Get(x => x.Id == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public IEnumerable<Comment> GetAll(Func<Comment, bool> predicate)
        {
            return _commentRepository.GetWhere(predicate);
        }

        public void Update(Comment comment)
        {
            if (!CheckIfCityExist(comment.CityId))
            {
                throw new NotFoundCityException();
            }
            else
            {
                _commentRepository.Update(comment);
            }
        }

        private bool CheckIfCityExist(int cityId)
        {
            return _cityRepository.Get(x => x.Id == cityId) == null ? false : true;
        }
    }
}
