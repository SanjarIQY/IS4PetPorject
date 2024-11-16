
namespace Courses.MVC.Controllers
{
    using System.Diagnostics;
    using Courses.MVC.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The main and controller that have 2 actions.
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
        /// Retrieves content from the specified URL using an HTTP client.
        /// </summary>
        /// <param name="url">The URL to fetch content from.</param>
        /// <returns>A <see cref="HttpResponseMessage"/> representing the response from the URL.</returns>
        private async Task<HttpResponseMessage> GetContentAsync(string url)
        {
            var client = this.clientFactory.CreateClient();
            return await client.GetAsync(url);
        }

        /// <summary>
        /// Retrieves the list of students from an external service.
        /// </summary>
        /// <returns>The students view with fetched content.</returns>
        [Route("GetStudents")]
        [Authorize]
        public async Task<IActionResult> GetStudentsAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:7000/Home/GetStudents");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Retrieves the average score of students from an external service.
        /// </summary>
        /// <returns>The average score view with fetched content.</returns>
        [Route("GetAverageProgress")]
        [Authorize]
        public async Task<IActionResult> GetAverageProgressAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:7000/Home/GetAverageProgress");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Retrieves the top 3 students from an external service.
        /// </summary>
        /// <returns>The top 3 students view with fetched content.</returns>
        [Route("GetActiveStudents")]
        [Authorize]
        public async Task<IActionResult> GetActiveStudentsAsync()
        {
            HttpResponseMessage message = await this.GetContentAsync("https://localhost:7000/Home/Top3ActiveStudents");
            this.ViewBag.Message = await message.Content.ReadAsStringAsync();
            return this.View();
        }

        /// <summary>
        /// Represent default controller.
        /// </summary>
        /// <returns>View to the this action.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Return demo courses.
        /// </summary>
        /// <returns>List of courses.</returns>
        public string[] GetCourses()
        {
            string[] courses = new string[]
            {
                "Asp.Net course",
                "Java sping course",
                "Javascript React course",
                "Angular Typescript course",
            };

            return courses;
        }

        /// <summary>
        /// List of mentors assigned to courses.
        /// </summary>
        /// <returns>List of mentors.</returns>
        public string[] GetMentors()
        {
            string[] mentors = new string[]
            {
                "John Doe - .NET Expert",
                "Jane Smith - Java Spring Specialist",
                "Mark Lee - React and JavaScript Tutor",
                "Sarah Connor - Angular & TypeScript Mentor",
            };

            return mentors;
        }

        /// <summary>
        /// Represents subscription types.
        /// </summary>
        /// <returns>Subscription types as string array.</returns>
        public string[] GetSubscriptions()
        {
            string[] subscriptions = new string[]
            {
                "Basic Subscription - $50",
                "Premium Subscription - $100",
            };

            return subscriptions;
        }

    }
}