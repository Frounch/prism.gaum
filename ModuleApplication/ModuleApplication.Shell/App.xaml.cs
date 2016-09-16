using System;
using System.Windows;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace ModuleApplication.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Init log4net
            InitLog4Net();

            // Launch bootstrapper
            var bootstrapper = new BootStrapper();
            bootstrapper.Run();
        }

        private static void InitLog4Net()
        {
            //Create file appender
            var appender = new FileAppender
            {
                Name = "RollingFileAppender",
                File = string.Format(@"Logs\{0:yyyy-MM-dd-HH-mm-ss}.txt", DateTime.Now),
                AppendToFile = true,
            };

            //Configure the layout of the trace message write
            var fileLayout = new PatternLayout { ConversionPattern = "%date - %-5level - [%logger] - %message%newline" };
            appender.Layout = fileLayout;
            fileLayout.ActivateOptions();
            appender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(appender);

#if DEBUG
            // Create console appender
            var consoleAppender = new ConsoleAppender();
            var consoleLayout = new PatternLayout { ConversionPattern = "LOG4NET - %-5level - [%logger] - %message%newline" };
            consoleAppender.Layout = consoleLayout;
            consoleLayout.ActivateOptions();

            //Let log4net configure itself based on the values provided
            consoleAppender.ActivateOptions();
            BasicConfigurator.Configure(consoleAppender);
#endif
        }
    }
}
