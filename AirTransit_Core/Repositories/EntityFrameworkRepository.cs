namespace AirTransit_Core.Repositories
{
    internal abstract class EntityFrameworkRepository
    {
        protected readonly MessagingContext MessagingContext;

        protected EntityFrameworkRepository(MessagingContext messagingContext)
        {
            this.MessagingContext = messagingContext;
        }

        protected void Commit()
        {
            this.MessagingContext.SaveChanges();
        }
    }
}