﻿using Data.Context;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class AlgoritmeRepo
    {
        private IAlgoritmeContext algoritmeContext;

        public AlgoritmeRepo(IAlgoritmeContext algoritmeContext)
        {
            this.algoritmeContext = algoritmeContext;
        }

        public List<Algoritme> ActiverenSysteem()
        {
          return  IAlgoritmeContext.ActiverenSysteem();
        }
    }
}
