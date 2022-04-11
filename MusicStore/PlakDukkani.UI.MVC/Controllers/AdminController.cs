using Microsoft.AspNetCore.Http;
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
    public class AdminController : Controller
    {
        //dependency injection
        IAlbumBLL albumBLL;
        IGenreBLL genreBLL;
        IArtistBLL artistBLL;
        public AdminController(IAlbumBLL albumBLL, IArtistBLL artistService, IGenreBLL genreService)
        {
            this.albumBLL = albumBLL;
            this.genreBLL = genreService;
            this.artistBLL = artistService;
        }
        // GET: AdminController
        public ActionResult Index()
        {
            ResultService<List<AlbumDetailVM>> albums = albumBLL.GetAlbumsDetailed();
            if (!albums.HasError)
                return View(albums.Data);
            else
                ViewBag.Message = albums.Errors[0].ErrorMessage;
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumDetailVM album)
        {
            try
            {
                int ArtistID = artistBLL.GetArtistID(album.FullName);
                int GenreID = genreBLL.GetGenreID(album.GenreName);
                album.GenreName = GenreID.ToString();
                album.FullName = ArtistID.ToString();

                ResultService<bool> createdAlbum = albumBLL.CreateAlbum(album);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            ResultService<AlbumDetailVM> result = albumBLL.GetAlbumById(id);
            return View(result.Data);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AlbumDetailVM album)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ResultService<bool> check =albumBLL.UpdateAlbum(album);
                    if (!check.Data)
                        ViewBag.Message = "Güncellenemedi.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            ResultService<AlbumDetailVM> result = albumBLL.GetAlbumById(id);
            return View(result.Data);
        }

        // POST: AdminController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAlbum(int id)
        {
            try
            {
                ResultService<bool> check = albumBLL.DeleteAlbum(id);
                if (!check.Data)
                    ViewBag.Message = "Silinemedi.";
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
