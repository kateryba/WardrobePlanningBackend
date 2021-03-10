
using System.Collections.Generic;

namespace WebApi.Dtos
{
    public class FamilyMember
    {
        public int ID { get; set; }

        public string Name { get;  set; }

        public int Age { get; set; }

        public int Size { get; set; }

        public List<Cloth> Wardrobe { get; set; }
    }
}
