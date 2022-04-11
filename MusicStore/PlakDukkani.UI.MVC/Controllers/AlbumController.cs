using Microsoft.AspNetCore.Mvc;
using PlakDukkani.BLL.Abstract;
using PlakDukkani.BLL.Concrete.ResultServiceBLL;
using PlakDukkani.ViewModel.AlbumViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlakDukkani.UI.MVC.Controllers
{
    public class AlbumController : Controller
    {
        IAlbumBLL albumBLL;
       
        public AlbumController(IAlbumBLL albumBLL, IArtistBLL artistService, IGenreBLL genreService)
        {
            this.albumBLL = albumBLL;
        }
        public IActionResult Index()
        {
            ResultService<List<AllAlbumsVM>> albumsResult = albumBLL.GetAllAlbums();
            if (!albumsResult.HasError)
            {
                return View(albumsResult.Data);
            }
            else
            {
                ViewBag.Message = albumsResult.Errors[0].ErrorMessage;
                return View();
            }
        }
    }
}


