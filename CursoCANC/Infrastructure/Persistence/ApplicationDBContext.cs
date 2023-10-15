using Application.Data;
using Domain.Customers;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDBContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher ?? throw new ArgumentException(nameof(publisher));
        }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var domainEvents = ChangeTracker.Entries<AgregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEventsCollection().Any())
                .SelectMany(e => e.GetDomainEventsCollection());

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach(var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }
    }
}
