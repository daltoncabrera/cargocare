 using System;
using System.Collections.Generic;
using System.Text;
using MSESG.CargoCare.Core;

namespace MSESG.CargoCare.Core.EFServices
{
    public class UpdaterTrackRepository : Repository<UpdaterTrack>
    {
        public UpdaterTrackRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
