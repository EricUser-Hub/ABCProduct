using Product.Infrastructure.Context;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private ProductDbContext context = new();
        private ProductRepository? productRepository;
        
        public ProductRepository ProductRepository
        {
            get
            {
                productRepository ??= new ProductRepository(context);
                return productRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
