using System;
using System.Collections.Generic;
using System.Text;

namespace BandBookerWasm.Shared.Models
{
    public class Musician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Instrument> Instruments { get; set; } = new List<Instrument>();
    }
}
