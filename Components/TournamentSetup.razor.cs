using System.Data;
using Blazorise;
using TournamentCreator.Services;

namespace TournamentCreator.Components
{
    public partial class TournamentSetup
    {
        public ITournamentSetup? TournamentSetupService { get; set; }
        private string TournamentName { get; set; } = string.Empty;
        private string[]? Teams { get; set;  }
        private string PointSystem { get; set; } = string.Empty;
        private Data.Tournament Model { get; set; } = new();

        protected override void OnInitialized()
        {
            TournamentSetupService = new Services.TournamentSetup();
        }

        public void ValidateNumberOfTeams(ValidatorEventArgs e)
        {
            Model.Teams = Convert.ToString(e.Value)!.Split(new [] { ";", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            e.Status = Model.Teams.Length is >= 4 and <= 8 ? ValidationStatus.Success : ValidationStatus.Error;
        }

        public void CreateTournament()
        {
            if (Model.Teams is null)
            {
                return;
            }

            if (Model.Teams.Length is < 4 or > 8 || string.IsNullOrEmpty(Model.Name))
            {
                return;
            }
            TournamentSetupService!.Create(Model);
            NavigationManager.NavigateTo("/schedule", true);
        }

        public Task OnCheckedValueChanged(string value)
        {
            Model.PointSystem = value;
            return Task.CompletedTask;
        }
    }
}