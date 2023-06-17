using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public interface IStepsService
    {
        public Task<int> UpdateStepsCountAsync(int userId, int stepsCount);
        public Task<StepsCountResponse> GetUsersStepsCount(int userId);
        public Task<bool> UpdateProgressAndLevel(int userId, int stepsCount);
        public Task<int> GetUsersLevel(int userId);
        public Task<int> GetUsersProgress(int userId);
        public Task<int> GetUsersChestsCount(int userId);
    }
}
