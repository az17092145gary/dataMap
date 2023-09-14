using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySqlConnector;
using slnLeagueTeamMap.Models;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;

namespace slnLeagueTeamMap.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly Services _service;


		public HomeController(ILogger<HomeController> logger, Services service)
		{
			_logger = logger;
			_service = service;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		[HttpPost]
		public dynamic Update(IEnumerable<formdata> formData)
		{
			using (MySqlConnection connection = new MySqlConnection(_service._connectionString))
			{
				connection.Open();
				var lists = _service.GetDatas(connection);
				connection.Close();
				return lists;
			}
		}

		[HttpGet]
		public dynamic GetDataAll()
		{
			using (MySqlConnection connection = new MySqlConnection(_service._connectionString))
			{
				connection.Open();
				var lists = _service.GetDatas(connection);
				connection.Close();
				return lists;
			}
		}
		[HttpPost]
		public dynamic GetleagueAndIdData(string id)
		{

			using (MySqlConnection connection = new MySqlConnection(_service._connectionString))
			{
				connection.Open();
				var lists = _service.GetleagueAndIdData(connection, id);
				connection.Close();
				return lists;
			}
		}
		[HttpPost]
		public dynamic Edit(string match_id, int id) 
		{
			using (MySqlConnection connection = new MySqlConnection(_service._connectionString)) 
			{
				connection.Open();
				//_service.Edit(connection, match_id, id);
				var list = _service.GetDatas(connection);
				connection.Close();
				return list;	
			}
		}
	}
}