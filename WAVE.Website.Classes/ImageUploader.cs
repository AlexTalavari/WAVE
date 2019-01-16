using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using ImageResizer;
using WAVE.Dal.Entities;
using WAVE.Dal.Infrastructure;
using WAVE.Dal.Repositories;

namespace WAVE.Website.Classes
{
    public static class ImageUploader
    {
        private static readonly IRepository<ImageData> ImageDataRepository;


        static ImageUploader()
        {
            ImageDataRepository = new Repository<ImageData>();
        }

        public static ImageData SaveFile(HttpPostedFileBase file, User owner)
        {
            // Verify that the user selected a file
            //That is less than 2MB and is a valid image
            if (file != null && file.ContentLength > 0 && file.ContentLength < 4*1024*1024 &&
                file.InputStream.IsValidImage())
            {
                // extract only the fielname                       
                var fileName = Path.GetFileName(file.FileName);
                if (fileName == null) return null;
                string pic = fileName.Substring(0, 4) + "-" +
                             DateTime.Now.ToString("yyyyMMddHHmmss");
                string path = Path.Combine(
                    HttpContext.Current.Server.MapPath("~/Uploads/Images/"), pic);
                file.SaveAs(path + Path.GetExtension(file.FileName));
                //Declare a new dictionary to store the parameters for the image versions.
                var versions = new Dictionary<string, string>
                {
                    {"_small", "maxwidth=600&maxheight=600&format=jpg"},
                    {"_medium", "maxwidth=900&maxheight=900&format=jpg"},
                    {"_large", "maxwidth=1200&maxheight=1200&format=jpg"}
                };
                //Define the versions to generate
                //Generate each version
                foreach (string suffix in versions.Keys)
                {
                    file.InputStream.Seek(0, SeekOrigin.Begin);

                    //Let the image builder add the correct extension based on the output file type
                    ImageBuilder.Current.Build(
                        new ImageJob(
                            file.InputStream,
                            path + file.FileName + suffix,
                            new Instructions(versions[suffix]),
                            false,
                            true));
                }

                var imgData = new ImageData
                {
                    Approved = true,
                    Owner = owner,
                    Url = @"/Uploads/Images/" + pic + Path.GetExtension(file.FileName)
                };
                ImageDataRepository.Add(imgData);
                return imgData;
            }
            return null;
        }
    }
}