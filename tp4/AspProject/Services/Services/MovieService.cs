﻿using AspProject.Models;
using AspProject.Repositories.RepositoryContracts;
using AspProject.Services.ServiceContracts;

namespace AspProject.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public Movie GetMovieById(Guid id)
        {
            return _movieRepository.GetMovieById(id);
        }

        public void CreateMovie(Movie movie)
        {
            _movieRepository.CreateMovie(movie);
        }

        public void Edit(Movie movie)
        {
            _movieRepository.EditMovie(movie);
        }

        public void Delete(Guid id)
        {
            _movieRepository.DeleteMovie(id);
        }

        public List<Movie> GetMoviesByGenre(Guid genreId)
        {
            return _movieRepository.GetMoviesByGenre(genreId);
        }

        public List<Movie> GetAllMoviesOrderedAscending()
        {
            return _movieRepository.GetAllMoviesOrderedAscending();
        }

        public List<Movie> GetMoviesByUserDefinedGenre(string userDefinedGenre)
        {
            return _movieRepository.GetMoviesByUserDefinedGenre(userDefinedGenre);
        }
    }
}
