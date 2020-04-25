using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BandBookerWasm.Shared.Models;
using Microsoft.AspNetCore.Components;
using BandBookerWasm.Client.Shared;
using BandBookerWasm.Shared;
using Microsoft.AspNetCore.Components.Web;
using DevExpress.Blazor;

namespace BandBookerWasm.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public BandBookerDataManager DataMan { get; set; }

        /* Instruments code */

        public List<Instrument> Instruments { get => DataMan.Instruments; }
        protected void InstrumentInsertAction(IDictionary<string, object> values)
        {            
            Instruments.Add(values.AssignToObject(new Instrument() { Id = Instruments.Count() + 1 }));
        }
		protected void InstrumentUpdateAction(Instrument instrument, IDictionary<string, object> newValues)
        {
            newValues.AssignToObject(instrument);
        }
        protected void InstrumentRemoveAction(Instrument instrument)
        {
            Instruments.Remove(instrument);
        }

        /* Musicians code */
        DxDataGrid<Musician> gridMusicians = null;
        Guid keyMusicians = Guid.NewGuid();
        public List<Musician> Musicians { get => DataMan.Musicians; }
        MusicianEditModel MusicianModel = null;
        protected void OnMusicianEditing(Musician musician)
        {
            MusicianModel = new MusicianEditModel(musician, DataMan.Instruments);
            MusicianModel.StateHasChanged += () => { InvokeAsync(StateHasChanged); };
        }
        protected void OnMusicianRemoving(Musician musician)
        {
            Musicians.Remove(musician);
        }
        protected void MusicianSubmitAction()
        {
            MusicianModel.Musician.Name = MusicianModel.Name;
            MusicianModel.Musician.Instruments = MusicianModel.Instruments.ToList();
            if (MusicianModel.IsNewRow)
            {
                DataMan.Musicians.Add(MusicianModel.Musician);
                keyMusicians = Guid.NewGuid();
            }
            MusicianModel = null;
            gridMusicians.CancelRowEdit();
        }
        void OnMusicianCancelButtonClick()
        {
            MusicianModel = null;
            gridMusicians.CancelRowEdit();
        }
    }
}