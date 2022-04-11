using PlakDukkani.BLL.Abstract;
using PlakDukkani.DAL.Abstract;
using PlakDukkani.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlakDukkani.BLL.Concrete
{
    class GenreService : IGenreBLL
    {
        IGenreDAL genreDAL;
        public GenreService(IGenreDAL genreDal)
        {
            this.genreDAL = genreDal;
        }
        public int GetGenreID(string genreName)
        {


            Genre genre;
            genre = genreDAL.Get(a => a.Name == genreName);

            if (genre == null)
            {
                genreDAL.Add(new Genre { Name = genreName, Description = "." });
                genre = genreDAL.Get(a => a.Name == genreName);
                return genre.ID;

            }
            return genre.ID;
        }
    }
}
