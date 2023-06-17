using Microsoft.EntityFrameworkCore;
using OneMoreStepAPI.Data;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
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

        public async Task<int> UpdateStepsCountAsync(int userId, int stepsCount)
        {
            var usersStepsCount = await _dbContext.UsersStepsCount.FirstOrDefaultAsync(c => (c.UserId == userId)
                                                                               && (c.Date.Date == DateTime.Today));
            int delta = -1;
            if (usersStepsCount == null)
            {
                usersStepsCount = new UsersStepsCount
                {
                    UserId = userId,
                    StepsCount = stepsCount,
                    Date = DateTime.Today
                };
                _dbContext.UsersStepsCount.Add(usersStepsCount);
                delta = stepsCount;
                
            }
            else
            {
                delta = stepsCount - usersStepsCount.StepsCount;
                usersStepsCount.StepsCount = stepsCount;
                _dbContext.UsersStepsCount.Update(usersStepsCount);
            }

            try
            {
                await _dbContext.SaveChangesAsync();
                return delta;
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }

        public async Task<bool> UpdateProgressAndLevel(int userId, int delta)
        {
            var curProgress = await _dbContext.Progress.FirstOrDefaultAsync(p => p.UserId == userId);
            if(curProgress == null)
            {
                await _dbContext.Progress.AddAsync(new Progress() { UserId = userId, UsersProgress = 0 });
                await _dbContext.SaveChangesAsync();
                curProgress = await _dbContext.Progress.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            var progress = curProgress.UsersProgress + delta;
            var addLevel = progress / Util.STEPS_FOR_LEVEL;
            if(addLevel != 0)
            {
                var currentLevel = await _dbContext.Levels.FirstOrDefaultAsync(l => l.UserId == userId);
                if (currentLevel == null)
                {
                    await _dbContext.Levels.AddAsync(new Level() { UserId = userId, Lvl = 0 });
                    await _dbContext.SaveChangesAsync();
                    currentLevel = await _dbContext.Levels.FirstOrDefaultAsync(p => p.UserId == userId);
                }
                var chestsNumber = 0;
                for (int i = currentLevel.Lvl + 1; i <= currentLevel.Lvl + addLevel; i++)
                {
                    if(i % 5 == 0)
                    {
                        chestsNumber++;
                    }
                }
                    
                currentLevel.Lvl += addLevel;
               
                if(chestsNumber > 0)
                {
                    var chests = await _dbContext.Chests.FirstOrDefaultAsync(l => l.UserId == userId);
                    if (chests == null)
                    {
                        await _dbContext.Chests.AddAsync(new Chest() { UserId = userId, ChestNumber = 1 });
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        chests.ChestNumber += chestsNumber;
                        _dbContext.Chests.Update(chests);
                    }
                }
                _dbContext.Levels.Update(currentLevel);
            }
            curProgress.UsersProgress = progress % Util.STEPS_FOR_LEVEL;
            _dbContext.Progress.Update(curProgress);
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<StepsCountResponse> GetUsersStepsCount(int userId)
        {
            DateTime currentDate = DateTime.Now;
            DateTime oneWeekAgo = currentDate.AddDays(-7); // Одна неделя назад относительно текущей даты
            DateTime oneMonthAgo = currentDate.AddMonths(-1);

            int stepsCountForWeek = await _dbContext.UsersStepsCount
                .Where(u => u.UserId == userId && u.Date >= oneWeekAgo && u.Date <= currentDate)
                .SumAsync(u => u.StepsCount);

            int stepsCountForMonth = await _dbContext.UsersStepsCount
                .Where(u => u.UserId == userId && u.Date >= oneMonthAgo && u.Date <= currentDate)
                .SumAsync(u => u.StepsCount);
            return new StepsCountResponse { ForMonth = stepsCountForMonth, ForWeek = stepsCountForWeek };
        }

        public async Task<int> GetUsersLevel(int userId)
        {
            var currentLevel = await _dbContext.Levels.FirstOrDefaultAsync(l => l.UserId == userId);
            if (currentLevel == null)
            {
                await _dbContext.Levels.AddAsync(new Level() { UserId = userId, Lvl = 0 });
                await _dbContext.SaveChangesAsync();
                currentLevel = await _dbContext.Levels.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            return currentLevel.Lvl;
        }

        public async Task<int> GetUsersProgress(int userId)
        {
            var curProgress = await _dbContext.Progress.FirstOrDefaultAsync(p => p.UserId == userId);
            if (curProgress == null)
            {
                await _dbContext.Progress.AddAsync(new Progress() { UserId = userId, UsersProgress = 0 });
                await _dbContext.SaveChangesAsync();
                curProgress = await _dbContext.Progress.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            return curProgress.UsersProgress;
        }

        public async Task<int> GetUsersChestsCount(int userId)
        {
            var chests = await _dbContext.Chests.FirstOrDefaultAsync(p => p.UserId == userId);
            if (chests == null)
            {
                await _dbContext.Chests.AddAsync(new Chest() { UserId = userId,  ChestNumber = 1 });
                await _dbContext.SaveChangesAsync();
                chests = await _dbContext.Chests.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            return chests.ChestNumber;
        }
    }
}
