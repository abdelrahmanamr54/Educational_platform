using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;

namespace Educational_platform.Repository
{
    public class ContactusRepositery : IContactusRepositery
    {
        private readonly ApplicationDbContext context;
        public ContactusRepositery(ApplicationDbContext context)
        {
            this.context = context;
        }


        public List<Contactus> getAllMsg()
        {
            var contact = context.contactus.ToList();
            return contact;
        }

        public Contactus findContact(int id)
        {
            var contact = context.contactus.Find(id);
            return contact;
        }
        public void  EditContact(Contactus contactus)
        {
      var contact = context.contactus.Find(contactus.Id);

            if (contact != null)
            {
                contact.Id = contactus.Id;

                contact.Status = contactus.Status;
                context.SaveChanges();
            }

        }
        public void AddContact(Contactus contactus)
        {
            context.contactus.Add(contactus);
            context.SaveChanges();

        }



    }
}
