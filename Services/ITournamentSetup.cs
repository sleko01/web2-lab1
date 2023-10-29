using TournamentCreator.Data;

namespace TournamentCreator.Services;

public interface ITournamentSetup
{
    public Tournament Create(Tournament tournament);
    public Tournament UpdateTable(Tournament tournament);
    public void InitializeTournament();
}

public class TournamentSetup : ITournamentSetup
{
    public Tournament Create(Tournament tournament)
    {
        var schedule = new List<List<Match>>();
        var teams = tournament.Teams.ToList();
        
        var numberOfTeams = teams.Count;
        if (numberOfTeams % 2 != 0)
        {
            teams.Add("Bye"); 
            numberOfTeams++;
        }
        
        var teamsList = new List<Team>();
        foreach (var team in teams)
        {
            if (team == "Bye") continue;
            teamsList.Add(new Team
            {
                Name = team,
                Points = 0
            });
        }
        for (var round = 0; round < numberOfTeams - 1; round++)
        {
            var roundMatches = new List<Match>();

            for (var i = 0; i < numberOfTeams / 2; i++)
            {
                var team1Index = i;
                var team2Index = numberOfTeams - 1 - i;

                if (teams[team1Index] != "Bye" && teams[team2Index] != "Bye")
                {
                    roundMatches.Add(new Match
                    {
                        Team1 = teamsList[team1Index],
                        Team2 = teamsList[team2Index],
                        Team1Score = "-1",
                        Team2Score = "-1"
                    });
                }
            }
            schedule.Add(roundMatches);

            // Rotate teams for the next round
            var lastTeam = teams[numberOfTeams - 1];
            for (var i = numberOfTeams - 1; i > 1; i--)
            {
                teams[i] = teams[i - 1];
            }
            teams[1] = lastTeam;

            var lastTeam2 = teamsList[numberOfTeams - 1];
            for (var i = numberOfTeams - 1; i > 1; i--)
            {
                teamsList[i] = teamsList[i - 1];
            }
            teamsList[1] = lastTeam2;
        }
        foreach (var round in schedule)
        {
            foreach (var match in round)
            {
                Console.WriteLine(match.ToString());
            }
        }
        tournament.Schedule = schedule;
        tournament.Standings = teamsList;
        Tournament.CurrentTournament = tournament;
        return tournament;
    }

    public Tournament UpdateTable(Tournament tournament)
    {
        var pointSystem = tournament.PointSystem;
        var pointList = new List<double>();
        switch (pointSystem)
        {
            case "football":
                pointList.Add(3);
                pointList.Add(1);
                break;
            case "chess":
                pointList.Add(1);
                pointList.Add(0.5);
                break;
            case "basketball":
                pointList.Add(2);
                pointList.Add(1);
                break;
            default:
                Console.WriteLine("Invalid point system");
                break;
        }
        foreach (var team in tournament.Standings)
        {
            team.Points = 0;
        }
        foreach(var round in tournament.Schedule)
        {
            foreach(var match in round)
            {
                if(int.Parse(match.Team1Score) == -1 || int.Parse(match.Team2Score) == -1)
                {
                    continue;
                }
                if (int.Parse(match.Team1Score) > int.Parse(match.Team2Score))
                {
                    match.Team1.Points += pointList[0];
                }
                else if (int.Parse(match.Team1Score) < int.Parse(match.Team2Score))
                {
                    match.Team2.Points += pointList[0];
                }
                else
                {
                    match.Team1.Points += pointList[1];
                    match.Team2.Points += pointList[1];
                }
            }
        }
        return tournament;
    }
    public void InitializeTournament()
    {
        var tournament = new Tournament
        {
            Name = "Prvenstvo Hrvatske u baseballu 2023.",
            Teams = new[] { "Baseball klub Zagreb", "Baseball klub Vindija Varazdin", "Baseball klub Olimpija Karlovac", "Baseball klub Nada Split", "Baseball klub Medvednica", "Baseball klub Blue Volves", "Baseball klub Sisak Storks", "Baseball klub Novi Zagreb"},
            PointSystem = "football"
        };
        tournament = Create(tournament);
        var r = new Random();
        for (var i = 0; i < 2; i++)
        {
            //populate first two rounds
            foreach(var match in tournament.Schedule[i])
            {
                match.Team1Score = r.Next(0, 5).ToString();
                match.Team2Score = r.Next(0, 5).ToString();
            }
        }
        
        tournament = UpdateTable(tournament);
    }
}