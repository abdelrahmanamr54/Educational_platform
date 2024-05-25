using Educational_platform.Models;

namespace Educational_platform.IRepositery
{
    public interface IContactusRepositery
    {
        List<Contactus> getAllMsg();


        Contactus findContact(int id);

        void EditContact(Contactus contactus);




      void AddContact(Contactus contactus);
    }
}
