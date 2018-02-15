namespace AirTransit_Core.Repositories
{
    abstract class EntityFrameworkRepository
    {
        protected readonly MessagingContext MessagingContext;

        public EntityFrameworkRepository(MessagingContext messagingContext)
        {
            MessagingContext = messagingContext;
        }

        protected void Commit()
        {
            this.MessagingContext.SaveChanges();
        }
    }
}