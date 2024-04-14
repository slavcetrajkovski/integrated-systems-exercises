using MovieApp.Domain.Model;
using MovieApp.Service.Interface;
using MovieAppRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Impl
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Movie CreateNewMovie(Movie t)
        {
            return this.movieRepository.Insert(t);
        }

        public Movie DeleteMovie(Movie m)
        {
            return this.movieRepository.Delete(m);
        }

        public Movie GetMovieById(Guid? Id)
        {
            return this.movieRepository.Get(Id);
        }

        public List<Movie> ListAllMovies()
        {
            return this.movieRepository.GetAll().ToList();
        }

        public Movie UpdateMovie(Movie t)
        {
            return this.movieRepository.Update(t);
        }
    }
}
