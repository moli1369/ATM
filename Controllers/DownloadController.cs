using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public FileResult File(Guid id)
        {
            // byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\folder\myfile.ext");
            using (var db = new MainModel())
            {
                var file =
                    db.Files.Find(id);
                return File(file.Bytes,
                    file.Type
                    // System.Net.Mime.MediaTypeNames.Application.Octet,
                    // fileName
                    );
            }
        }
    }
}