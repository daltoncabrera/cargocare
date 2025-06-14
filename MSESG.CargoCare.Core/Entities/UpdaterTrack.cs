using System;
using System.Collections.Generic;
using System.Text;

namespace MSESG.CargoCare.Core
{
    public class UpdaterTrack : Updateable
    {
        public EntityType Type { get; set; }
        public string  Object { get; set; }
    }
}
