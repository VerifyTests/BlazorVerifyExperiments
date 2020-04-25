using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BandBookerWasm.Shared.Models;
using Microsoft.AspNetCore.Components;
using BandBookerWasm.Client.Shared;
using BandBookerWasm.Shared;

namespace BandBookerWasm.Client.Pages
{
    public partial class DXIndex : ComponentBase
    {
        public Instrument SelectedInstrument;

        public void InstrumentSelected(ChangeEventArgs args)
        {
            string instrumentId = args.Value.ToString();

            SelectedInstrument =
                (from x in Instruments
                 where x.Id.ToString() == instrumentId.ToString()
                 select x).First();

            if (SelectedInstrument != null)
                DisableInstrumentEditButton = false;
            else
                DisableInstrumentEditButton = true;
        }

        public bool DisableInstrumentEditButton = true;

        public InstrumentEditor instrumentEditor;
        public string InstrumentErrorMessage = "";
        public bool DisableInstrumentList = false;

        public void InstrumentCancelled(string message)
        {
            InstrumentErrorMessage = message;
            DisableInstrumentList = false;
            DisableInstrumentEditButton = (SelectedInstrument == null);
        }

        public void InstrumentAdded(Instrument instrument)
        {
            //Instruments.Add(instrument);
            //SelectedInstrument = instrument;
            //DisableInstrumentList = false;
            //DisableInstrumentEditButton = false;
            //instrumentEditor.Hide();
        }

        public void InstrumentUpdated(string message)
        {
            //DisableInstrumentList = false;
            //DisableInstrumentEditButton = false;
            //instrumentEditor.Hide();
        }

        public void NewInstrumentButtonClick()
        {
            InstrumentErrorMessage = "";
            //DisableInstrumentList = true;
            DisableInstrumentEditButton = true;
            //SelectedInstrument = new Instrument();
            instrumentEditor.NewInstrument();
        }

        public void EditInstrumentButtonClick()
        {
            //DisableInstrumentList = true;
            DisableInstrumentEditButton = true;
            instrumentEditor.EditInstrument(SelectedInstrument);
        }

        public void DeleteInstrumentButtonClick()
        {
            if (SelectedInstrument != null)
            {
                Instruments.Remove(SelectedInstrument);
                if (Instruments.Count > 0)
                {
                    SelectedInstrument = Instruments.First();
                }
            }
        }

        public List<Instrument> AllInstruments = new List<Instrument>();
        public Musician SelectedMusician;
        //public MusicianEditor musicianEditor;
        public DXMusicianEditor musicianEditor; // <-- changed
        public bool DisableMusicianControls = false;
        public bool DisableMusicianEditButton = true;

        public async Task NewMusicianButtonClick()
        {
            DisableMusicianControls = true;
            DisableMusicianEditButton = true;
            AllInstruments.Clear();
            foreach (var inst in Instruments)
            {
                AllInstruments.Add(inst);
            }
            await musicianEditor.NewMusician(AllInstruments);
        }

        public async Task EditMusicianButtonClick()
        {
            DisableMusicianControls = true;
            DisableMusicianEditButton = true;
            AllInstruments.Clear();
            foreach (var inst in Instruments)
            {
                AllInstruments.Add(inst);
            }
            await musicianEditor.EditMusician(AllInstruments, SelectedMusician);
        }

        public void DeleteMusicianButtonClick()
        {
            if (SelectedMusician != null)
            {
                Musicians.Remove(SelectedMusician);
                if (Musicians.Count >= 1)
                {
                    SelectedMusician = Musicians.First();
                }
            }
        }

        public void MusicianSelected(ChangeEventArgs e)
        {
            var Id = Convert.ToInt32(e.Value);
            SelectedMusician =
                (from x in Musicians
                 where x.Id.ToString() == Id.ToString()
                 select x).FirstOrDefault();

            if (SelectedMusician != null)
            {
                DisableMusicianEditButton = false;
            }
            else
                DisableMusicianEditButton = true;
        }

        public void EditMusicianCancelled(string message)
        {
            if (SelectedMusician != null)
            {
                SelectedMusician =
                (from x in Musicians
                 where x.Id == SelectedMusician.Id
                 select x).FirstOrDefault();
            }
            DisableMusicianControls = false;
            DisableMusicianEditButton = (SelectedMusician == null);
        }

        public void MusicianAdded(Musician Musician)
        {
            Musicians.Add(Musician);
            DisableMusicianControls = false;
            DisableMusicianEditButton = (SelectedMusician == null);
        }

        public void MusicianUpdated(Musician musician)
        {
            SelectedMusician = musician;
            DisableMusicianControls = false;
            DisableMusicianEditButton = false;
        }

        public List<Instrument> Instruments = new List<Instrument>();
        public List<Musician> Musicians = new List<Musician>();

        protected override void OnInitialized()
        {
            // Create Instruments
            Instruments = new List<Instrument>();
            Instrument instrument;

            instrument = new Instrument
            {
                Id = 1,
                Name = "Guitar"
            };
            Instruments.Add(instrument);

            instrument = new Instrument
            {
                Id = 2,
                Name = "Keyboards"
            };
            Instruments.Add(instrument);

            instrument = new Instrument
            {
                Id = 3,
                Name = "Bass"
            };
            Instruments.Add(instrument);

            instrument = new Instrument
            {
                Id = 4,
                Name = "Drums"
            };
            Instruments.Add(instrument);

            // Musicians 
            Musicians = new List<Musician>();
            Musician musician;

            musician = new Musician
            {
                Id = 1,
                Name = "Piano Patty"
            };
            Musicians.Add(musician);

            musician = new Musician
            {
                Id = 2,
                Name = "Shredding Shelly"
            };
            Musicians.Add(musician);

            musician = new Musician
            {
                Id = 3,
                Name = "Thumping Theo"
            };
            Musicians.Add(musician);

            musician = new Musician
            {
                Id = 4,
                Name = "Banging Bob"
            };
            Musicians.Add(musician);
        }
    }
}
