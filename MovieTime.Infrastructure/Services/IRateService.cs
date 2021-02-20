using MovieTime.Core.Domain;
using MovieTime.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTime.Infrastructure.Services
{
   public interface IRateService
    {
        RateDto Get(Guid ID);
        RateDto Create(Guid ID, Guid UserID, Guid MovieID, int Value);
        void Update(Guid ID, Guid UserID, Guid MovieID, int Value);
        void Delete(Guid ID);
        IEnumerable<RateDto> GetAllByUserId(Guid UserID);
    }
}
