using System;
using Microsoft.EntityFrameworkCore;

namespace School.Services.Identity.Infrastructure.Interfaces
{
    public interface IUnitOfWork<T> where T : DbContext, IDisposable
    {

        void Save();
    }
}