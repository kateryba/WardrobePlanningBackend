using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class Cloth
    {
        public int ID { get; set; }

        public int Size { get; set; }

        public string Type { get; set; }

        public string Owner { get; set; }

        public string Color { get; set; }

        public string Season { get; set; }

        public string Style { get; set; }

        public string Fit { get; set; }

        public string Comment { get; set; }

    }
}
