using OneMoreStepAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IStepsService
    {
        public Task<bool> UpdateStepsCountAsync(int userId, int stepsCount);
        public Task<int> GetUsersStepsCount(int userId, int daysNumber);
    }
}
