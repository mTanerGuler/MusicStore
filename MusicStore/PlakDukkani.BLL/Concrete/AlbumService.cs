using PlakDukkani.BLL.Abstract;
using PlakDukkani.BLL.Concrete.ResultServiceBLL;
using PlakDukkani.DAL.Abstract;
using PlakDukkani.Model.Entities;
using PlakDukkani.ViewModel.AlbumViewModels;
using PlakDukkani.ViewModel.CartViewModels.CartItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlakDukkani.BLL.Concrete
{
    public class AlbumService : IAlbumBLL
    {
        IAlbumDAL albumDAL;
        public AlbumService(IAlbumDAL albumDAL)
        {
            this.albumDAL = albumDAL;
        }
        public ResultService<List<SingleAlbumVM>> GetSingleAlbums()
        {
            ResultService<List<SingleAlbumVM>> resultService = new ResultService<List<SingleAlbumVM>>();
            try
            {
                List<SingleAlbumVM> singleAlbums = albumDAL.GetAll(a => a.IsActive && a.Continued, a => a.Artist)
                        .OrderByDescending(a => a.CreatedDate).Take(12)
                        .Select(album => new SingleAlbumVM
                        {
                            ID = album.ID,
                            FullName = album.Artist.FullName,
                            AlbumArtUrl = album.AlbumArtUrl,
                            Price = album.Price,
                            Title = album.Title
                        }).ToList();
                resultService.Data = singleAlbums;
            }
            catch (Exception ex)
            {
                resultService.AddError("exception", ex.Message);
            }

            return resultService;
        }

        public ResultService<List<AllAlbumsVM>> GetAllAlbums()
        {
            ResultService<List<AllAlbumsVM>> resultService = new ResultService<List<AllAlbumsVM>>();
            try
            {
                List<AllAlbumsVM> allAlbums = albumDAL.GetAll(a => a.IsActive, a => a.Artist)
                    .OrderBy(a => a.Title)
                    .Select(a =>
                    new AllAlbumsVM
                    {
                        Title = a.Title,
                        AlbumArtUrl = a.AlbumArtUrl,
                        FullName = a.Artist.FullName,
                        ID = a.ID
                    }
                    ).ToList();
                resultService.Data = allAlbums;
            }
            catch (Exception ex)
            {
                resultService.AddError("exception", ex.Message);
            }
            return resultService;
        }

        public ResultService<AlbumDetailVM> GetAlbumById(int id)
        {
            ResultService<AlbumDetailVM> result = new ResultService<AlbumDetailVM>();
            Album album=albumDAL.Get(a => a.ID == id && a.Continued && a.IsActive, a => a.Artist, x => x.Genre);
            if (album == null)
            {
                result.AddError("Null Hatası","id ile uyumlu album yok");
                return result;
            }
            result.Data = new AlbumDetailVM()
            {
                ID=album.ID,
                Title=album.Title,
                GenreName=album.Genre.Name,
                AlbumArtUrl=album.AlbumArtUrl,
                Discount=album.Discount,
                FullName=album.Artist.FullName,
                Price=album.Price
            };
            return result;
        }

        public ResultService<CartItem> GetCartById(int id)
        {
            ResultService<CartItem> result = new ResultService<CartItem>();
            Album album = albumDAL.Get(a => a.ID == id && a.Continued && a.IsActive);
            if (album==null)
            {
                result.AddError("Null Hatası", "id ile uyumlu album yok");
                return result;
            }
            result.Data = new CartItem()
            {
                ID = album.ID,
                Title = album.Title,
                Discount = album.Discount,
                Price = album.Price
            };
            return result;
        }

        public ResultService<List<AlbumDetailVM>> GetAlbumsDetailed()
        {
            ResultService<List<AlbumDetailVM>> resultService = new ResultService<List<AlbumDetailVM>>();
            try
            {
                List<AlbumDetailVM> albumList = albumDAL.GetAll(null, x => x.Artist,x=>x.Genre)
                                                        .OrderByDescending(a=>a.CreatedDate)
                                                        .Select(album=>new AlbumDetailVM
                                                        { 
                                                            AlbumArtUrl=album.AlbumArtUrl,
                                                            Discount=album.Discount,
                                                            FullName=album.Artist.FullName,
                                                            GenreName=album.Genre.Name,
                                                            ID=album.ID,
                                                            Price=album.Price,
                                                            Title=album.Title
                                                        }
                                                        ).ToList();
                resultService.Data = albumList;
            }
            catch (Exception ex)
            {
                resultService.AddError("exception", ex.Message);
            }
            return resultService;
        }

        public ResultService<bool> UpdateAlbum(AlbumDetailVM album)
        {
            ResultService<bool> result = new ResultService<bool>();
            Album updatedAlbum=albumDAL.Get(a=>a.ID==album.ID, x=>x.Artist,x=>x.Genre);
            updatedAlbum.Title = album.Title;
            updatedAlbum.Price = album.Price;
            updatedAlbum.Genre.Name = album.GenreName;
            updatedAlbum.Artist.FullName = album.FullName;
            updatedAlbum.Discount = album.Discount;
            updatedAlbum.AlbumArtUrl = album.AlbumArtUrl;
            Album checkAlbum = albumDAL.Update(updatedAlbum);
            if (checkAlbum != null)
                result.Data = true;
            else
                result.Data = false;
            return result;
        }

        public ResultService<bool> CreateAlbum(AlbumDetailVM album)
        {
            ResultService<bool> result = new ResultService<bool>();
            Album createdAlbum = new Album() 
            {
                AlbumArtUrl=album.AlbumArtUrl,
                Discount = album.Discount,
                ArtistID= Convert.ToInt32(album.FullName),
                GenreID= Convert.ToInt32(album.GenreName),
                Price = album.Price,
                Title = album.Title
            };
            Album checkAlbum = albumDAL.Add(createdAlbum);
            if (checkAlbum != null)
                result.Data = true;
            else
                result.Data = false;
            return result;
        }

        public ResultService<bool> DeleteAlbum(int id)
        {
            ResultService<bool> result = new ResultService<bool>();
            Album deletedAlbum = albumDAL.Get(a => a.ID == id);
            int check=albumDAL.Remove(deletedAlbum);
            if (check > 0)
                result.Data = true;
            else
                result.Data = false;
            return result;
        }
    }
}
