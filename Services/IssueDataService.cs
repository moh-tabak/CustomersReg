using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersReg.Models;
using Microsoft.EntityFrameworkCore;
namespace CustomersReg.Services
{
    internal interface IIssueDataService
    {
        Task<IEnumerable<Issue>> GetAllIssuesAsync();
        Task AddIssueAsync(Issue issue);
    }

    internal class IssueDataService : IIssueDataService
    {
        public async Task AddIssueAsync(Issue issue)
        {
            using var context = new SqlContext();
            context.Issues.Add(issue);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            List<Issue> issues = new List<Issue>();
            using (var context = new SqlContext())
            {
                issues = await context.Issues.OrderByDescending(t => t.CreationDate).ToListAsync();
            }
            return issues;
        }
    }
}
