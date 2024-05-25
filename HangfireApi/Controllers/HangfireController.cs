using Hangfire;
using HangfireApi.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace HangfireApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangfireController : Controller
    {
        [HttpGet]
        [Route("CreateJob")]
        public ActionResult CreateJob()
        {
            //BackgroundJob.Enqueue(() => Console.WriteLine("backgroud job runing"));
            BackgroundJob.Enqueue<TestJob>(x => x.WriteLog("backgroud job runing"));
            return Ok();
        }

        [HttpGet]
        [Route("CreateSceduleJob")]
        public ActionResult ScheduleJob()
        {
            var dateNow = DateTime.UtcNow;
            var dateRun = dateNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(dateRun);
            //BackgroundJob.Schedule(() => Console.WriteLine("backgroud job scedule " + DateTime.UtcNow + " createdScedule" + dateNow), dateTimeOffset);
            BackgroundJob.Schedule<TestJob>(x => x.WriteLog("backgroud job scedule " + DateTime.UtcNow + " createdScedule" + dateNow), dateTimeOffset);
            Console.WriteLine("Created " + DateTime.UtcNow + " run" + dateRun);
            return Ok();
        }

        [HttpGet]
        [Route("ParalelJob")]
        public ActionResult ParalelJob ()
        {
            var dateNow = DateTime.UtcNow;
            var dateRun = dateNow.AddMilliseconds(1);
            var dateTimeOffset = new DateTimeOffset(dateRun);
            var job1 = BackgroundJob.Schedule(() => Console.WriteLine("backgroud job scedule " + DateTime.UtcNow + " createdScedule" + dateNow), dateTimeOffset);
            var job2 = BackgroundJob.ContinueJobWith(job1, () => Console.WriteLine("Continew job 2"));
            var job3 = BackgroundJob.ContinueJobWith(job2, () => Console.WriteLine("Continew job 3"));
            var job4 = BackgroundJob.ContinueJobWith(job3, () => Console.WriteLine("Continew job 4"));
            var job5 = BackgroundJob.ContinueJobWith(job3, () => Console.WriteLine("Continew job 5"));
            return Ok();
        }


        [HttpGet]
        [Route("berulang")]
        public ActionResult BerulangJob()
        {
            RecurringJob.AddOrUpdate("Berulang Job 1", () => Console.WriteLine("Berulang " + DateTime.Now), "* * * * *");
            return Ok();
        }
    }
}
