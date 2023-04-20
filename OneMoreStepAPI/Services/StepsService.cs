using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services
{
    public class StepsService: BaseService, IStepsService
    {
        public StepsService(OneMoreStepAPIDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> UpdateStepsCountAsync(int userId, int stepsCount)
        {
            var usersStepsCount = await _dbContext.UsersStepsCount.FirstOrDefaultAsync(c => (c.UserId == userId)
                                                                               && (c.Date.Date == DateTime.Today));

            if (usersStepsCount == null)
            {
                usersStepsCount = new UsersStepsCount
                {
                    UserId = userId,
                    StepsCount = stepsCount,
                    Date = DateTime.Today
                };
                _dbContext.UsersStepsCount.Add(usersStepsCount);
                
            }
            else
            {
                _dbContext.UsersStepsCount.Update(usersStepsCount);
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public async Task<int> GetUsersStepsCount(int userId, int daysNumber)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user == null) return -1;

            var usersStepsCount = await _dbContext.UsersStepsCount.Where(s => s.UserId == userId &&
                                                                 (DateTime.Now.Date - s.Date.Date).TotalDays < 1)
                                                                 .SumAsync(s => s.StepsCount);
            return usersStepsCount;
        }
    }
}
