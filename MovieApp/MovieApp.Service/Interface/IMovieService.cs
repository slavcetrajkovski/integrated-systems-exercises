using MovieApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Interface
{
    public interface IMovieService
    {
        public List<Movie> ListAllMovies();
        public Movie GetMovieById(Guid? Id);
        public Movie CreateNewMovie(Movie t);
        public Movie UpdateMovie(Movie t);
        public Movie DeleteMovie(Movie m);
    }
}
