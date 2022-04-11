using PlakDukkani.BLL.Abstract;
using PlakDukkani.DAL.Abstract;
using PlakDukkani.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlakDukkani.BLL.Concrete
{
    class ArtistService : IArtistBLL
    {
        IArtistDAL artistDal;
        public ArtistService(IArtistDAL artistDal)
        {
            this.artistDal = artistDal;
        }
        public int GetArtistID(string artistFullName)
        {
            Artist artist;
            artist = artistDal.Get(a => a.FullName == artistFullName);

            if (artist == null)
            {
                artistDal.Add(new Artist { FullName = artistFullName, Bio = "." });
                artist = artistDal.Get(a => a.FullName == artistFullName);
                return artist.ID;

            }
            return artist.ID;
        }
    }
}
