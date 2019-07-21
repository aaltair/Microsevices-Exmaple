using System;
using Microsoft.EntityFrameworkCore;

namespace School.Services.Courses.Infrastructure.Interfaces
{
    public interface IUnitOfWork<T> where T : DbContext, IDisposable
    {

        void Save();
    }
}