using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class PartsId
    {
        public PartsId()
        {
            this.Parts = new List<int>();
        }
        public List<int> Parts { get; set; }
    }
}
