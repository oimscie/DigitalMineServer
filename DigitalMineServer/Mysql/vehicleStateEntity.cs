using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Mysql
{
    public class vehicleStateEntity
    {
        public string VEHICLE_ID { get; set; }
        public string VEHICLE_SIM { get; set; }
        public string VEHICLE_TYPE { get; set; }
        public string VEHICLE_DRIVER { get; set; }
        public string POSI_STATE { get; set; }
        public string POSI_X { get; set; }
        public string POSI_Y { get; set; }
        public string POSI_SPEED { get; set; }
        public string REAl_FUEL { get; set; }
        public string ACC { get; set; }
        public string POSI_NUM { get; set; }
        public string COMPANY { get; set; }
        public string ADD_TIME { get; set; }
    }
}
