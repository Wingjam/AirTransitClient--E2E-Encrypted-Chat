namespace AirTransit_Core.Repositories
{
    public class EntityFrameworkRepository
    {
        protected MessagingContext MessagingContext;

        protected void Commit()
        {
            this.MessagingContext.SaveChanges();
        }
    }
}