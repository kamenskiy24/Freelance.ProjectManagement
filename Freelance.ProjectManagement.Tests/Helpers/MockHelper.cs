using Freelance.ProjectManagement.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace Freelance.ProjectManagement.Tests.Helpers
{
    public class MockHelper
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : BaseEntity<int>
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = sourceList.AsQueryable().BuildMockDbSet();

            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(s => sourceList.Add(s));
            dbSet.Setup(d => d.AddAsync(It.IsAny<T>(), default)).Callback<T, CancellationToken>((s, ct) => sourceList.Add(s));
            dbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(s => sourceList.Remove(s));
            dbSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).Returns((object[] r) => new ValueTask<T?>(sourceList.FirstOrDefault(i => i.Id == (int) r[0] )));

            return dbSet.Object;
        }
    }
}
