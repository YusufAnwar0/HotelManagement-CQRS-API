using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Service
{
    public class TransactionalContext : ITransactionalContext
    {
        private readonly Context _context;
        private IDbContextTransaction _currentTransaction;
        public TransactionalContext(Context context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _context.Database.CommitTransactionAsync(cancellationToken);
                }

            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }

            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync(cancellationToken);
                }
            }
            finally
            {
                if ( _currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
