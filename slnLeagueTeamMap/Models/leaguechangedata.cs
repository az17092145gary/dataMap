namespace slnLeagueTeamMap.Models
{
	public class leaguechangedata
	{
		public int? id { get; set; }
		public DateTime? kick_off_time { get; set; }
		public string? league_name { get; set; }
		public int? leagues_id { get; set; }

		public int? home_team_id { get; set; }
		public string? home { get; set; }
		public int? away_team_id { get; set; }
		public string? away { get; set; }


	}
}
