using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.DTO.RabbitMQ
{
    public class QueueMessageRabbitMQ
    {
        //public bool Start => true;
        public int Id { get; set; }

        public QueueMessageRabbitMQ() { }

        public QueueMessageRabbitMQ(int id)
        {
            Id = id;
        }
    }
}
