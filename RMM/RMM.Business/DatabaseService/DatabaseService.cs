﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;

namespace RMM.Business.DatabaseService
{
  public  class DatabaseService : IDatabaseService
    {
        public void Initialize()
        {
            RmmConfiguration.Initialize();
        }
    }
}
