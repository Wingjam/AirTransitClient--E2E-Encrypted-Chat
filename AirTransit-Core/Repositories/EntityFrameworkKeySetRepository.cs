namespace AirTransit_Core.Repositories
{
    class EntityFrameworkKeySetRepository : EntityFrameworkRepository, IKeySetRepository
    {
        public EntityFrameworkKeySetRepository(MessagingContext messagingContext) : base(messagingContext) { }
    }
}