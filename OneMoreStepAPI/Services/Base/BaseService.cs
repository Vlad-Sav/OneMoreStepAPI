using OneMoreStepAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Services.Base
{
    public class BaseService
    {
        protected OneMoreStepAPIDbContext _dbContext;

        public BaseService(OneMoreStepAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
