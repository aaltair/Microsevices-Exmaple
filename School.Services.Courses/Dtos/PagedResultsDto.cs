using System.Collections.Generic;

namespace School.Services.Courses.Dtos
{
    public class PagedResultsDto<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}