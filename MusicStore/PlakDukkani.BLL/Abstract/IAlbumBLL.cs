using PlakDukkani.BLL.Concrete.ResultServiceBLL;
using PlakDukkani.Model.Entities;
using PlakDukkani.ViewModel.AlbumViewModels;
using PlakDukkani.ViewModel.CartViewModels.CartItems;
using System.Collections.Generic;

namespace PlakDukkani.BLL.Abstract
{
    public interface IAlbumBLL : IBaseBLL<Album>
    {
        ResultService<List<SingleAlbumVM>> GetSingleAlbums();
        ResultService<List<AllAlbumsVM>> GetAllAlbums();
        ResultService<AlbumDetailVM> GetAlbumById(int id);
        ResultService<CartItem> GetCartById(int id);
        ResultService<List<AlbumDetailVM>> GetAlbumsDetailed();
        ResultService<bool> UpdateAlbum(AlbumDetailVM album);
        ResultService<bool> CreateAlbum(AlbumDetailVM album);
        ResultService<bool> DeleteAlbum(int id);
    }
}
