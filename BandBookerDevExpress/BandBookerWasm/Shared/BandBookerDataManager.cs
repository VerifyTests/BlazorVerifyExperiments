using BandBookerWasm.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BandBookerWasm.Shared
{
	public class BandBookerDataManager
	{
		public BandBookerDataManager()
		{
			Musicians = new List<Musician>(new Musician[] {
				new Musician { Id = 1, Name = "Piano Patty" },
				new Musician { Id = 2, Name = "Shredding Shelly" },
				new Musician { Id = 3, Name = "Thumping Theo" },
				new Musician { Id = 4, Name = "Banging Bob" }
			});

			Instruments = new List<Instrument>(new Instrument[] {
				new Instrument { Id = 1, Name = "Guitar" },
				new Instrument { Id = 2, Name = "Keyboards" },
				new Instrument { Id = 3, Name = "Bass" },
				new Instrument { Id = 4, Name = "Drums" }
			});
        }

        public List<Musician> Musicians { get; private set; }

		public List<Instrument> Instruments { get; private set; }
	}
}
