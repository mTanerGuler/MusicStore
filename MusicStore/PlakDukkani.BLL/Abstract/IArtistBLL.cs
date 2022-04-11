using PlakDukkani.Model.Entities;

namespace PlakDukkani.BLL.Abstract
{
    public interface IArtistBLL : IBaseBLL<Artist>
    {
        int GetArtistID(string fullName);
    }
}
