using CRUD.DAL.Models.Identity;

namespace CRUD.PL.Helpers
{
    public interface ImailSettings
    {
        public void SendMail(Email email);
    }
}
