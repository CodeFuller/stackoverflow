using System;
using System.Web.Http;
using Microsoft.Win32.TaskScheduler;

namespace WebApiApplication.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values
		public string Get()
		{
			string powerShellScript = @"d:\Dropbox\prog\StackOverflow\Q49094779\script.ps1";
			string userName = @"BYM1D100\testuser";
			string userPassword = "TestPwd123";

			using (TaskService ts = new TaskService())
			{
				TaskDefinition td = ts.NewTask();
				td.Triggers.Add(new RegistrationTrigger
				{
					StartBoundary = DateTime.Now,
					EndBoundary = DateTime.Now.AddMinutes(1),
				});
				td.Settings.DeleteExpiredTaskAfter = TimeSpan.Zero;
				td.Actions.Add(new ExecAction("powershell.exe", powerShellScript));
				ts.RootFolder.RegisterTaskDefinition($@"Print Pdf - {Guid.NewGuid()}", td, createType: TaskCreation.Create, userId: userName, password: userPassword, logonType: TaskLogonType.Password);
			}

			return "Scheduled";
		}
	}
}
