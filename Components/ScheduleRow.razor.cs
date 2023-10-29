using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using TournamentCreator.Data;
using TournamentCreator.Services;

namespace TournamentCreator.Components
{
    public partial class ScheduleRow
    {
        [Required]
        [Parameter]
        public string Team1 { get; set; } = string.Empty;
        [Required]
        [Parameter]
        public string Team2 { get; set; } = string.Empty;
        [Required]
        [Parameter]
        public int Score1 { get; set; }
        [Required]
        [Parameter]
        public int Score2 { get; set; }
        
        private Match _model = new();
        public ITournamentSetup? TournamentSetupService { get; set; }

        protected override void OnInitialized()
        {
            TournamentSetupService = new Services.TournamentSetup();
            foreach (var round in Tournament.CurrentTournament.Schedule)
            {
                foreach (var match in round)
                {
                    if (Team1 == match.Team1.Name && Team2 == match.Team2.Name)
                    {
                        _model = match;
                        break;
                    }
                }
            }
        }

        public void ChangeScores()
        {
            for (var i = 0; i < Tournament.CurrentTournament.Schedule.Count; i++)
            {
                for (var j = 0; j < Tournament.CurrentTournament.Schedule[i].Count; j++)
                {
                    if (_model.Team1 == Tournament.CurrentTournament.Schedule[i][j].Team1 && _model.Team2 == Tournament.CurrentTournament.Schedule[i][j].Team2)
                    {
                        Tournament.CurrentTournament.Schedule[i][j] = _model;
                        Tournament.CurrentTournament = TournamentSetupService!.UpdateTable(Tournament.CurrentTournament);
                        NavigationManager.NavigateTo("/schedule", true);
                        return;
                    }
                }
            }
        }
    }
}