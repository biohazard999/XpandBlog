using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win;
using System.Collections.Generic;
using XpandBlog.Module;
using XpandBlog.Module.Win;

namespace XpandBlog.Win
{
    public  class XpandBlogWindowsFormsApplication : WinApplication
    {
        public XpandBlogWindowsFormsApplication()
        {
            this.ApplicationName = "XpandBlog";

            this.Modules.Add(new SystemModule());
            this.Modules.Add(new SystemWindowsFormsModule());
            this.Modules.Add(new XpandBlogModule());
            this.Modules.Add(new XpandBlogWindowsFormsModule());
        }

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args)
        {
            args.ObjectSpaceProvider = new XPObjectSpaceProvider(args.ConnectionString, args.Connection);
        }

        protected override void OnCustomizeLanguages(IList<string> languages)
        {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (userLanguageName != "en-US" && languages.IndexOf(userLanguageName) == -1)
            {
                languages.Add(userLanguageName);
            }

            base.OnCustomizeLanguages(languages);
        }

        protected override void OnDatabaseVersionMismatch(DatabaseVersionMismatchEventArgs args)
        {
#if EASYTEST
            args.Updater.Update();
            args.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached)
            {
                args.Updater.Update();
                args.Handled = true;
            }
            else
            {
                throw new InvalidOperationException(
                    "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application.\r\n" +
                    "This error occurred  because the automatic database update was disabled when the application was started without debugging.\r\n" +
                    "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " +
                    "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " +
                    "or manually create a database using the 'DBUpdater' tool.\r\n" +
                    "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " +
                    "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/");
            }
#endif

            base.OnDatabaseVersionMismatch(args);
        }
    }
}
