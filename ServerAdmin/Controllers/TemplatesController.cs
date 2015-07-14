using System.Web.Mvc;
using System.Web.UI.WebControls;
using GleamTech.FileUltimate;

namespace ServerAdmin.Controllers
{
    public class TemplatesController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult HeaderNotification()
        {
            return View();
        }

        public ActionResult Sidebar()
        {
            return View();
        }

        public ActionResult SidebarSearch()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Stat()
        {
            return View();
        }

        public ActionResult Timeline()
        {
            return View();
        }

        public ActionResult Notification()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }

        public ActionResult FileExplorer()
        {
            FileManager fileManager = new FileManager
            {
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                Resizable = true,
                LicenseKey = "1F72GKKXBZ1FV-3AZTPON3M7NFS-3NR3S3JK8VRYK-25OX90VM7UH8V"

            };

            //Create a root folder via assignment statements and add it to the control.
            FileManagerRootFolder rootFolder = new FileManagerRootFolder();
            rootFolder.Name = "1. Root Folder";

            rootFolder.Location = "d:\\";

            fileManager.RootFolders.Add(rootFolder);
            FileManagerAccessControl accessControl = new FileManagerAccessControl();
            accessControl.Path = @"\";
            accessControl.AllowedPermissions = FileManagerPermissions.Full;
            rootFolder.AccessControls.Add(accessControl);

            return View(fileManager);
        }
    }
}