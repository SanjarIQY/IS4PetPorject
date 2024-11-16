namespace Students.MVC.Controllers
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Students.MVC.Models;

    /// <summary>
    /// Controller for managing home page and student-related actions.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IHttpClientFactory clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance to log application messages.</param>
        /// <param name="clientFactory">The client factory for creating HTTP clients.</param>
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            this.logger = logger;
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The home view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Retrieves content from the specified URL using an HTTP client.
        /// </summary>
        /// <param name="url">The URL to fetch content from.</param>
        /// <returns>A <see cref="HttpResponseMessage"/> representing the response from the URL.</returns>
        public async Task<HttpResponseMessage> GetContentAsync(string url)
        {
            var client = this.clientFactory.CreateClient();
            var response = await client.GetAsync(url);

            return response;
        }

        /// <summary>
        /// Retrieves the list of courses from an external service.
        /// </summary>
        /// <returns>The courses view with fetched content.</returns>
        [Authorize]
        [Route("GetCourses")]
        public async Task<IActionResult> GetCoursesAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:9090/home/GetCourses");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Retrieves the list of mentors from an external service.
        /// </summary>
        /// <returns>The mentors view with fetched content.</returns>
        [Authorize]
        [Route("GetMentors")]
        public async Task<IActionResult> GetMentorsAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:9090/home/GetMentors");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Retrieves the list of subscription options from an external service.
        /// </summary>
        /// <returns>The subscriptions view with fetched content.</returns>
        [Route("GetSubscriptions")]
        [Authorize]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:9090/home/GetSubscriptions");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Gets the average progress of all students.
        /// </summary>
        /// <returns>The average progress as a double value.</returns>
        public double GetAverageProgress()
        {
            return 78.0;
        }

        /// <summary>
        /// Gets the list of students.
        /// </summary>
        /// <returns>An array of student names.</returns>
        public string[] GetStudents()
        {
            return new string[]
            {
                "Amin Karimov",
                "Zafarbek Yunusov",
                "Alina Kamilova",
                "Daniyar Abdullayev",
                "Mariya Orlova",
                "Omarbek Tursunov",
                "Nargiza Rakhimova",
                "Rustambek Sattarov",
                "Zamira Ismailova",
                "Sherzodbek Mavlonov",
                "Bektur Sagynov",
                "Dilyara Khalilova",
                "Nodirbek Umarov",
                "Ravshanbek Tashkentov",
                "Nadira Kamolova",
                "Javlonbek Abdullayev",
                "Shakhzod Bekzodov",
                "Shirinbek Pirmatov",
                "Yulduz Ergasheva",
                "Otabek Nishonov",
            };
        }

        /// <summary>
        /// Gets the list of top 3 active students.
        /// </summary>
        /// <returns>An array of top 3 active student names.</returns>
        public string[] Top3ActiveStudents()
        {
            return new string[]
            {
                "Otabek Nishonov",
                "Shakhzod Bekzodov",
                "Mariya Orlova",
            };
        }
    }
}
