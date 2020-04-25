using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BandBookerWasm.Shared.Models;

namespace BandBookerWasm.Client
{
    public class MusicianEditModel
    {
        public MusicianEditModel(Musician musician, IEnumerable<Instrument> allInstruments)
        {
            Musician = musician;
            if (musician == null)
            {
                Musician = new Musician();
                IsNewRow = true;
            }

            Name = Musician.Name;
            Instruments = new List<Instrument>(Musician.Instruments);
            AllInstruments = new List<Instrument>(allInstruments);
        }

        public Action StateHasChanged { get; set; }
        public Musician Musician { get; set; }
        public bool IsNewRow { get; set; }

        public List<Instrument> AllInstruments { get; set; }

        // model properties
        [Required]
        public string Name { get; set; }
        public IEnumerable<Instrument> Instruments { get; set; }
    }
}
