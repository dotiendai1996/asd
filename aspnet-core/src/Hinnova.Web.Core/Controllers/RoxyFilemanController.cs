using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Abp.Runtime.Session;
using Abp.UI;
using Hinnova.Authorization.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

namespace Hinnova.Web.Controllers
{
    //[Route("api/[controller]/[action]"), Produces("application/json")]
    //[AbpMvcAuthorize]
    public class RoxyFilemanController : HinnovaControllerBase
    {
        private readonly string _systemRootPath;
        private readonly string _tempPath;
        private string _filesRootPath;
        private string _filesRootVirtual;
        private Dictionary<string, string> _settings;
        private Dictionary<string, string> _lang = null;
        private readonly UserManager _userManager;


        public RoxyFilemanController(IHostingEnvironment env, UserManager userManager)
        {
            _userManager = userManager;
            // Setup CMS paths to suit your environment (we usually inject settings for these)
            _systemRootPath = env.ContentRootPath;
            _tempPath = _systemRootPath + "\\wwwroot\\upload_files\\Temp";
            _filesRootPath = "/wwwroot/upload_files/Content";
            _filesRootVirtual = "/upload_files/Content";
            // Load Fileman settings
            LoadSettings();
        }

        private void LoadSettings()
        {
            _settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.IO.File.ReadAllText(_systemRootPath + "/wwwroot/fileman/conf.json"));
            string langFile = _systemRootPath + "/wwwroot/fileman/lang/" + GetSetting("LANG") + ".json";
            if (!System.IO.File.Exists(langFile))
            {
                langFile = _systemRootPath + "/wwwroot/fileman/lang/en.json";
            }

            _lang = JsonConvert.DeserializeObject<Dictionary<string, string>>(System.IO.File.ReadAllText(langFile));
        }

        // GET api/RoxyFileman - test entry point//]
        [AllowAnonymous, Produces("text/plain"), ActionName("")]
        public string Get() { return "RoxyFileman - access to API requires Authorisation"; }

        #region API Actions
        public IActionResult DIRLIST(string type)
        {
            try
            {
                if (GetCurrentUserId() == null)
                {
                    throw new Exception("Invalid files root directory. Check your configuration.");
                }
                DirectoryInfo d = new DirectoryInfo(GetFilesRoot());
                if (!d.Exists)
                {
                    throw new Exception("Invalid files root directory. Check your configuration.");
                }
                string rootDir = GetFilesRoot() + "/" + GetCurrentUserId();

                d = new DirectoryInfo(rootDir);

                if (!d.Exists)
                {
                    Directory.CreateDirectory(rootDir);
                    d = new DirectoryInfo(rootDir);
                }

                ArrayList dirs = ListDirs(d.FullName);
                dirs.Insert(0, d.FullName);
                string localPath = _systemRootPath;
                string result = "";
                for (int i = 0; i < dirs.Count; i++)
                {
                    string dir = (string)dirs[i];
                    result += (result != "" ? "," : "") + "{\"p\":\"" + MakeVirtualPath(dir.Replace(localPath, "").Replace("\\", "/")) + "\",\"f\":\"" + GetFiles(dir, type).Count.ToString() + "\",\"d\":\"" + Directory.GetDirectories(dir).Length.ToString() + "\"}";
                }
                return Content("[" + result + "]", "application/json");
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult FILESLIST(string d, string type)
        {
            try
            {
                var userId = GetCurrentUserId();

                string rootDir = _filesRootPath + "/" + GetCurrentUserId();
                if (!d.StartsWith(_filesRootPath + "/" + userId) && !d.StartsWith(_filesRootVirtual + "/" + userId))
                {
                    throw new Exception("Access to " + d + " is denied");
                }
                d = MakePhysicalPath(d);
                CheckPath(d);
                string fullPath = FixPath(d);
                List<string> files = GetFiles(fullPath, type);
                string result = "";
                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo f = new FileInfo(files[i]);
                    int w = 0, h = 0;
                    // NO SUPPORT IN ASP.NET CORE! Per haps see https://github.com/CoreCompat/CoreCompat
                    //if (GetFileType(f.Extension) == "image")
                    //{
                    //    try
                    //    {
                    //        //FileStream fs = new FileStream(f.FullName, FileMode.Open, FileAccess.Read);
                    //        //Image img = Image.FromStream(fs);
                    //        //w = img.Width;
                    //        //h = img.Height;
                    //        //fs.Close();
                    //        //fs.Dispose();
                    //        //img.Dispose();
                    //    }
                    //    catch (Exception ex) { throw ex; }
                    //}
                    result += (result != "" ? "," : "") +
                        "{" +
                        "\"p\":\"" + MakeVirtualPath(d) + "/" + f.Name + "\"" +
                        ",\"t\":\"" + Math.Ceiling(LinuxTimestamp(f.LastWriteTime)).ToString() + "\"" +
                        ",\"s\":\"" + f.Length.ToString() + "\"" +
                        ",\"w\":\"" + w.ToString() + "\"" +
                        ",\"h\":\"" + h.ToString() + "\"" +
                        "}";
                }
                return Content("[" + result + "]");
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult COPYDIR(string d, string n)
        {
            try
            {
                var userId = GetCurrentUserId();

                string rootDir = _filesRootPath + "/" + GetCurrentUserId();
                if (!d.StartsWith(_filesRootPath + "/" + userId) && !d.StartsWith(_filesRootVirtual + "/" + userId))
                {
                    throw new Exception("Access to " + d + " is denied");
                }
                if (!n.StartsWith(_filesRootPath + "/" + userId) || !n.StartsWith(_filesRootPath + "/" + userId))
                {
                    throw new Exception("Access to " + n + " is denied");
                }

                d = MakePhysicalPath(d);
                n = MakePhysicalPath(n);
                CheckPath(d);
                CheckPath(n);
                DirectoryInfo dir = new DirectoryInfo(FixPath(d));
                DirectoryInfo newDir = new DirectoryInfo(FixPath(n + "/" + dir.Name));
                if (!dir.Exists)
                {
                    throw new Exception(LangRes("E_CopyDirInvalidPath"));
                }
                else if (newDir.Exists)
                {
                    throw new Exception(LangRes("E_DirAlreadyExists"));
                }
                else
                {
                    CopyDir(dir.FullName, newDir.FullName);
                }

                return Content(GetSuccessRes());
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult COPYFILE(string f, string n)
        {
            try
            {
                var userId = GetCurrentUserId();

                string rootDir = _filesRootPath + "/" + GetCurrentUserId();
                if (!f.StartsWith(_filesRootPath + "/" + userId) || !f.StartsWith(_filesRootPath + "/" + userId))
                {
                    throw new Exception("Access to " + f + " is denied");
                }
                if (!n.StartsWith(_filesRootPath + "/" + userId) || !n.StartsWith(_filesRootPath + "/" + userId))
                {
                    throw new Exception("Access to " + n + " is denied");
                }


                f = MakePhysicalPath(f);
                CheckPath(f);
                FileInfo file = new FileInfo(FixPath(f));
                n = FixPath(n);
                if (!file.Exists)
                {
                    throw new Exception(LangRes("E_CopyFileInvalisPath"));
                }
                else
                {
                    try
                    {
                        System.IO.File.Copy(file.FullName, Path.Combine(n, MakeUniqueFilename(n, file.Name)));
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_CopyFile")); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult CREATEDIR(string d, string n)
        {
            try
            {
                d = MakePhysicalPath(d);
                CheckPath(d);
                d = FixPath(d);
                if (!Directory.Exists(d))
                {
                    throw new Exception(LangRes("E_CreateDirInvalidPath"));
                }
                else
                {
                    try
                    {
                        d = Path.Combine(d, n);
                        if (!Directory.Exists(d))
                        {
                            Directory.CreateDirectory(d);
                        }

                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_CreateDirFailed")); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult DELETEDIR(string d)
        {
            try
            {
                d = MakePhysicalPath(d);
                CheckPath(d);
                d = FixPath(d);
                if (!Directory.Exists(d))
                {
                    throw new Exception(LangRes("E_DeleteDirInvalidPath"));
                }
                else if (d == GetFilesRoot())
                {
                    throw new Exception(LangRes("E_CannotDeleteRoot"));
                }
                else if (Directory.GetDirectories(d).Length > 0 || Directory.GetFiles(d).Length > 0)
                {
                    throw new Exception(LangRes("E_DeleteNonEmpty"));
                }
                else
                {
                    try
                    {
                        Directory.Delete(d);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_CannotDeleteDir")); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult DELETEFILE(string f)
        {
            try
            {
                f = MakePhysicalPath(f);
                CheckPath(f);
                f = FixPath(f);
                if (!System.IO.File.Exists(f))
                {
                    throw new Exception(LangRes("E_DeleteFileInvalidPath"));
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(f);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_DeletеFile")); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public ActionResult DOWNLOAD(string f)
        {
            try
            {
                f = MakePhysicalPath(f);
                CheckPath(f);
                FileInfo file = new FileInfo(FixPath(f));
                if (file.Exists)
                {
                    new FileExtensionContentTypeProvider().TryGetContentType(file.FullName, out string contentType);
                    return PhysicalFile(file.FullName, contentType ?? "application/octet-stream", file.Name);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex) { return Json(GetErrorRes(ex.Message)); }
        }

        public ActionResult DOWNLOADDIR(string d)
        {
            try
            {
                var userId = GetCurrentUserId();

                string rootDir = _filesRootPath + "/" + GetCurrentUserId();
                if (!d.StartsWith(_filesRootPath + "/" + userId) && !d.StartsWith(_filesRootVirtual + "/" + userId))
                {
                    throw new Exception("Access to " + d + " is denied");
                }


                d = MakePhysicalPath(d);
                d = FixPath(d);
                if (!Directory.Exists(d))
                {
                    throw new Exception(LangRes("E_CreateArchive"));
                }

                string dirName = new FileInfo(d).Name;
                string tmpZip = _tempPath + "/" + dirName + ".zip";
                if (System.IO.File.Exists(tmpZip))
                {
                    System.IO.File.Delete(tmpZip);
                }

                ZipFile.CreateFromDirectory(d, tmpZip, CompressionLevel.Fastest, true);
                return PhysicalFile(tmpZip, "application/zip", dirName + ".zip");
            }
            catch (Exception ex) { return Json(GetErrorRes(ex.Message)); }
        }

        public IActionResult MOVEDIR(string d, string n)
        {
            try
            {
                d = MakePhysicalPath(d);
                n = MakePhysicalPath(n);
                CheckPath(d);
                CheckPath(n);
                DirectoryInfo source = new DirectoryInfo(FixPath(d));
                DirectoryInfo dest = new DirectoryInfo(FixPath(Path.Combine(n, source.Name)));
                if (dest.FullName.IndexOf(source.FullName) == 0)
                {
                    throw new Exception(LangRes("E_CannotMoveDirToChild"));
                }
                else if (!source.Exists)
                {
                    throw new Exception(LangRes("E_MoveDirInvalisPath"));
                }
                else if (dest.Exists)
                {
                    throw new Exception(LangRes("E_DirAlreadyExists"));
                }
                else
                {
                    try
                    {
                        source.MoveTo(dest.FullName);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_MoveDir") + " \"" + d + "\""); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult MOVEFILE(string f, string n)
        {
            try
            {
                f = MakePhysicalPath(f);
                n = MakePhysicalPath(n);
                CheckPath(f);
                CheckPath(n);
                FileInfo source = new FileInfo(FixPath(f));
                FileInfo dest = new FileInfo(FixPath(n));
                if (!source.Exists)
                {
                    throw new Exception(LangRes("E_MoveFileInvalisPath"));
                }
                else if (dest.Exists)
                {
                    throw new Exception(LangRes("E_MoveFileAlreadyExists"));
                }
                else if (!CanHandleFile(dest.Name))
                {
                    throw new Exception(LangRes("E_FileExtensionForbidden"));
                }
                else
                {
                    try
                    {
                        source.MoveTo(dest.FullName);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_MoveFile") + " \"" + f + "\""); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult RENAMEDIR(string d, string n)
        {
            try
            {
                d = MakePhysicalPath(d);
                CheckPath(d);
                DirectoryInfo source = new DirectoryInfo(FixPath(d));
                DirectoryInfo dest = new DirectoryInfo(Path.Combine(source.Parent.FullName, n));
                if (source.FullName == GetFilesRoot())
                {
                    throw new Exception(LangRes("E_CannotRenameRoot"));
                }
                else if (!source.Exists)
                {
                    throw new Exception(LangRes("E_RenameDirInvalidPath"));
                }
                else if (dest.Exists)
                {
                    throw new Exception(LangRes("E_DirAlreadyExists"));
                }
                else
                {
                    try
                    {
                        source.MoveTo(dest.FullName);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception) { throw new Exception(LangRes("E_RenameDir") + " \"" + d + "\""); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        public IActionResult RENAMEFILE(string f, string n)
        {
            try
            {
                f = MakePhysicalPath(f);
                CheckPath(f);
                FileInfo source = new FileInfo(FixPath(f));
                FileInfo dest = new FileInfo(Path.Combine(source.Directory.FullName, n));
                if (!source.Exists)
                {
                    throw new Exception(LangRes("E_RenameFileInvalidPath"));
                }
                else if (!CanHandleFile(n))
                {
                    throw new Exception(LangRes("E_FileExtensionForbidden"));
                }
                else
                {
                    try
                    {
                        source.MoveTo(dest.FullName);
                        return Content(GetSuccessRes());
                    }
                    catch (Exception ex) { throw new Exception(ex.Message + "; " + LangRes("E_RenameFile") + " \"" + f + "\""); }
                }
            }
            catch (Exception ex) { return Content(GetErrorRes(ex.Message)); }
        }

        [HttpPost, Produces("text/plain")]
        [AllowAnonymous]
        public string UPLOAD(string d)
        {
            try
            {
                d = MakePhysicalPath(d);
                CheckPath(d);
                d = FixPath(d);
                string res = GetSuccessRes();
                bool hasErrors = false;
                try
                {
                    foreach (Microsoft.AspNetCore.Http.IFormFile file in HttpContext.Request.Form.Files)
                    {
                        if (CanHandleFile(file.FileName))
                        {
                            FileInfo f = new FileInfo(file.FileName);
                            string filename = MakeUniqueFilename(d, f.Name);
                            string dest = Path.Combine(d, filename);
                            using (FileStream saveFile = new FileStream(dest, FileMode.Create))
                            {
                                file.CopyTo(saveFile);
                            }
                            //if (GetFileType(new FileInfo(filename).Extension) == "image")
                            //{
                            //    int w = 0;
                            //    int h = 0;
                            //    int.TryParse(GetSetting("MAX_IMAGE_WIDTH"), out w);
                            //    int.TryParse(GetSetting("MAX_IMAGE_HEIGHT"), out h);
                            //    ImageResize(dest, dest, w, h);
                            //}
                        }
                        else
                        {
                            hasErrors = true;
                            res = GetSuccessRes(LangRes("E_UploadNotAll"));
                        }
                    }
                }
                catch (Exception ex) { res = GetErrorRes(ex.Message); }
                if (IsAjaxUpload())
                {
                    if (hasErrors)
                    {
                        res = GetErrorRes(LangRes("E_UploadNotAll"));
                    }

                    return res;
                }
                else
                {
                    return "<script>parent.fileUploaded(" + res + ");</script>";
                }
            }
            catch (Exception ex)
            {
                if (!IsAjaxUpload())
                {
                    return "<script>parent.fileUploaded(" + GetErrorRes(LangRes("E_UploadNoFiles")) + ");</script>";
                }
                else
                {
                    return GetErrorRes(ex.Message);
                }
            }
        }

        /*
        public string GENERATETHUMB(string type)
        {
            try
            {
                //int w = 140, h = 0;
                //int.TryParse(_context.Request["width"].Replace("px", ""), out w);
                //int.TryParse(_context.Request["height"].Replace("px", ""), out h);
                //ShowThumbnail(_context.Request["f"], w, h);
            }
            catch (Exception ex) { return GetErrorRes(ex.Message); }
        }
        */
        #endregion

        #region Utilities
        private string MakeVirtualPath(string path)
        {
            return !path.StartsWith(_filesRootPath) ? path : _filesRootVirtual + path.Substring(_filesRootPath.Length);
        }

        private string MakePhysicalPath(string path)
        {
            return !path.StartsWith(_filesRootVirtual) ? path : _filesRootPath + path.Substring(_filesRootVirtual.Length);
        }

        private string GetFilesRoot()
        {
            string ret = _filesRootPath;
            try
            {
                if (GetSetting("SESSION_PATH_KEY") != "" && HttpContext.Session.GetString(GetSetting("SESSION_PATH_KEY")) != null)
                {
                    ret = HttpContext.Session.GetString(GetSetting("SESSION_PATH_KEY"));
                }

            }
            catch (Exception ex)
            {

            }

            ret = FixPath(ret);
            return ret;
        }

        public bool IsLoad = false;
        public string CurrentUserId { get; set; }
        private string GetCurrentUserId()
        {
            //if (!CurrentUserId.IsNullOrWhiteSpace()&& IsLoad)
            //{
            //    return CurrentUserId;
            //}
            var test = _userManager.GetUserAsync(AbpSession.ToUserIdentifier()).Result;
            if (test != null)
            {
                CurrentUserId = test.UserName + test.TenantId;
                IsLoad = true;
                return CurrentUserId;
            }

            throw new UserFriendlyException("There is no current user!");
            //return "abccc";
            HttpContext.Session.TryGetValue("UserId", out byte[] arrs);

            if (arrs == null)
            {
                return "";
            }

            string userId = arrs[0].ToString();

            return userId;
        }

        private ArrayList ListDirs(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            ArrayList ret = new ArrayList();
            foreach (string dir in dirs)
            {
                ret.Add(dir);
                ret.AddRange(ListDirs(dir));
            }
            return ret;
        }

        private List<string> GetFiles(string path, string type)
        {
            List<string> ret = new List<string>();
            if (type == "#" || type == null)
            {
                type = "";
            }

            string[] files = Directory.GetFiles(path);
            foreach (string f in files)
            {
                if ((GetFileType(new FileInfo(f).Extension) == type) || (type == ""))
                {
                    ret.Add(f);
                }
            }
            return ret;
        }

        private string GetFileType(string ext)
        {
            string ret = "file";
            ext = ext.ToLower();
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
            {
                ret = "image";
            }
            else if (ext == ".swf" || ext == ".flv")
            {
                ret = "flash";
            }

            return ret;
        }

        private void CheckPath(string path)
        {
            if (FixPath(path).IndexOf(GetFilesRoot()) != 0)
            {
                throw new Exception("Access to " + path + " is denied");
            }

            //var userId = GetCurrentUserId();

            //string rootDir = _filesRootPath + "/" + GetCurrentUserId();
            //if (!path.StartsWith(_filesRootPath + "/" + userId) || !path.StartsWith(_filesRootPath + "/" + userId))
            //{
            //    throw new Exception("Access to " + path + " is denied");
            //}
        }

        private string FixPath(string path)
        {
            path = path.TrimStart('~');
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }

            return _systemRootPath + path;
        }

        private double LinuxTimestamp(DateTime d)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
            TimeSpan timeSpan = (d.ToLocalTime() - epoch);
            return timeSpan.TotalSeconds;
        }

        private string GetSetting(string name)
        {
            string ret = "";
            if (_settings.ContainsKey(name))
            {
                ret = _settings[name];
            }

            return ret;
        }

        private string GetErrorRes(string msg) { return GetResultStr("error", msg); }

        private string GetResultStr(string type, string msg)
        {
            return "{\"res\":\"" + type + "\",\"msg\":\"" + msg.Replace("\"", "\\\"") + "\"}";
        }

        private string LangRes(string name) { return _lang.ContainsKey(name) ? _lang[name] : name; }

        private string GetSuccessRes(string msg) { return GetResultStr("ok", msg); }

        private string GetSuccessRes() { return GetSuccessRes(""); }

        private void CopyDir(string path, string dest)
        {
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            foreach (string f in Directory.GetFiles(path))
            {
                FileInfo file = new FileInfo(f);
                if (!System.IO.File.Exists(Path.Combine(dest, file.Name)))
                {
                    System.IO.File.Copy(f, Path.Combine(dest, file.Name));
                }
            }
            foreach (string d in Directory.GetDirectories(path))
            {
                CopyDir(d, Path.Combine(dest, new DirectoryInfo(d).Name));
            }
        }

        private string MakeUniqueFilename(string dir, string filename)
        {
            string ret = filename;
            int i = 0;
            while (System.IO.File.Exists(Path.Combine(dir, ret)))
            {
                i++;
                ret = Path.GetFileNameWithoutExtension(filename) + " - Copy " + i.ToString() + Path.GetExtension(filename);
            }
            return ret;
        }

        private bool CanHandleFile(string filename)
        {
            bool ret = false;
            FileInfo file = new FileInfo(filename);
            string ext = file.Extension.Replace(".", "").ToLower();
            string setting = GetSetting("FORBIDDEN_UPLOADS").Trim().ToLower();
            if (setting != "")
            {
                ArrayList tmp = new ArrayList();
                tmp.AddRange(Regex.Split(setting, "\\s+"));
                if (!tmp.Contains(ext))
                {
                    ret = true;
                }
            }
            setting = GetSetting("ALLOWED_UPLOADS").Trim().ToLower();
            if (setting != "")
            {
                ArrayList tmp = new ArrayList();
                tmp.AddRange(Regex.Split(setting, "\\s+"));
                if (!tmp.Contains(ext))
                {
                    ret = false;
                }
            }
            return ret;
        }

        private bool IsAjaxUpload()
        {
            return (!string.IsNullOrEmpty(HttpContext.Request.Query["method"]) && HttpContext.Request.Query["method"].ToString() == "ajax");
        }
        #endregion
    }
}