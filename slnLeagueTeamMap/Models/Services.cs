using Dapper;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace slnLeagueTeamMap.Models
{
	public class Services
	{
		public Services()
		{
		}
		public string _connectionString = "Server=localhost; Port=3306;user =root;pwd='az17092145'; Database = sportsbook_prod";
		public IEnumerable<data> GetDatas(MySqlConnection connection)
		{
			var datas = connection.Query<data>("SELECT mp.match_id, mp.kick_off_time, lp.league_name AS league1, lp.checked AS leaguecheck, IFNULL(l.league_name, '') AS league2, " +
				" thp.team_name AS home1,  thp.checked as homecheck, CASE WHEN mp.reverse = 1 THEN IFNULL(ta.team_name,'') ELSE IFNULL(th.team_name, '') END AS home2, tap.team_name AS away1," +
				" tap.checked as awaycheck, CASE WHEN mp.reverse = 1 THEN IFNULL(th.team_name, '') ELSE IFNULL(ta.team_name,'') END AS away2, m.id, m.leagues_id as mleagueid, mp.reverse, lp.league_id," +
				" thp.team_id AS Home_team_id, tap.team_id AS Away_team_id FROM match_map AS mp" +
				" INNER JOIN league_map AS lp ON mp.league_id = lp.league_id INNER JOIN team_map AS thp ON mp.home_team_id = thp.team_id " +
				" INNER JOIN team_map AS tap ON mp.away_team_id = tap.team_id INNER JOIN matches AS m ON mp.matches_rel_id = m.id " +
				" INNER JOIN leagues AS l ON m.leagues_id = l.id INNER JOIN teams AS th ON th.id = m.home_team_id INNER JOIN teams AS ta ON ta.id = m.away_team_id " +
				" WHERE mp.web_site = 1 AND mp.main_match_id = mp.match_id AND (thp.checked = 0 OR tap.checked = 0) ORDER BY mp.kick_off_time DESC;");
			return datas;
		}
		public void UpdateDatas(MySqlConnection connection, int homeTeamId, int awayTeamId, int leagueId)
		{
			string updateQuery = @"UPDATE sportsbook_prod.team_map 
									SET CHECKED = 1 
									WHERE team_id = @HomeTeamId OR team_id = @AwayTeamId;

									UPDATE sportsbook_prod.league_map 
									SET CHECKED = 1 
									WHERE web_site=1 AND league_id= @LeagueId;";
			connection.Execute(updateQuery, new { HomeTeamId = homeTeamId, AwayTeamId = awayTeamId, LeagueId = leagueId });
		}
		public IEnumerable<leaguechangedata> GetleagueAndIdData(MySqlConnection connection, string id)
		{
			var list = connection.Query<leaguechangedata>("get_matches", new { match_id = id }, commandType: CommandType.StoredProcedure);
			return list;
		}
		public void Edit(MySqlConnection connection, string match_id, int? id)
		{
			connection.Execute("Edit_match_map", new { match_id, id = (id == -1) ? null : id }, commandType: CommandType.StoredProcedure);
		}
	}
}
