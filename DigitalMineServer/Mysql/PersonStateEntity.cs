﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Mysql
{
    public class PersonStateEntity
    {
        public string PERSON_ID { get; set; }
        public string PERSON_SIM { get; set; }
        public string PERSON_TYPE { get; set; }
        public string POSI_STATE { get; set; }
        public string POSI_X { get; set; }
        public string POSI_Y { get; set; }
        public string ACC { get; set; }
        public string POSI_NUM { get; set; }
        public string COMPANY { get; set; }
        public string ADD_TIME { get; set; }
    }
}