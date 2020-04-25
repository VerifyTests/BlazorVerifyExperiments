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
    public partial class Index : ComponentBase
    {
        protected Instrument SelectedInstrument;
        protected InstrumentEditor instrumentEditor;
        protected string InstrumentErrorMessage = "";
        protected bool DisableInstrumentList = false;

        protected void InstrumentCancelled(string message)
        {
            InstrumentErrorMessage = message;
            DisableInstrumentList = false;
            DisableInstrumentEditButton = (SelectedInstrument == null);
        }

        protected void InstrumentAdded(Instrument instrument)
        {
            Instruments.Add(instrument);
            SelectedInstrument = instrument;
            DisableInstrumentList = false;
            DisableInstrumentEditButton = false;
            instrumentEditor.Hide();
        }

        protected void InstrumentUpdated(string message)
        {
            DisableInstrumentList = false;
            DisableInstrumentEditButton = false;
            instrumentEditor.Hide();
        }

        protected void InstrumentSelected(ChangeEventArgs args)
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

        protected async Task NewInstrumentButtonClick()
        {
            InstrumentErrorMessage = "";
            DisableInstrumentList = true;
            DisableInstrumentEditButton = true;
            SelectedInstrument = new Instrument();
            await instrumentEditor.NewInstrument();
        }

        protected async Task EditInstrumentButtonClick()
        {
            DisableInstrumentList = true;
            DisableInstrumentEditButton = true;
            await instrumentEditor.EditInstrument();
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

        protected List<Instrument> Instruments = new List<Instrument>();
        protected List<Musician> Musicians = new List<Musician>();

        protected List<Instrument> AllInstruments = new List<Instrument>();
        protected Musician SelectedMusician;
        protected MusicianEditor musicianEditor;
        protected bool DisableMusicianControls = false;
        protected bool DisableMusicianEditButton = true;

        protected void NewMusicianButtonClick()
        {
            DisableMusicianControls = true;
            DisableMusicianEditButton = true;
            AllInstruments.Clear();
            foreach (var inst in Instruments)
            {
                AllInstruments.Add(inst);
            }
            musicianEditor.NewMusician(AllInstruments);
        }

        protected void EditMusicianButtonClick()
        {
            DisableMusicianControls = true;
            DisableMusicianEditButton = true;
            AllInstruments.Clear();
            foreach (var inst in Instruments)
            {
                AllInstruments.Add(inst);
            }
            musicianEditor.EditMusician(AllInstruments, SelectedMusician);
        }

        protected void DeleteMusicianButtonClick()
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

        protected void MusicianSelected(ChangeEventArgs e)
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

        protected void EditMusicianCancelled(string message)
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

        protected void MusicianAdded(Musician Musician)
        {
            Musicians.Add(Musician);
            DisableMusicianControls = false;
            DisableMusicianEditButton = (SelectedMusician == null);
        }

        protected void MusicianUpdated(Musician musician)
        {
            SelectedMusician = musician;
            DisableMusicianControls = false;
            DisableMusicianEditButton = false;
        }

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