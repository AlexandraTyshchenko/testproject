using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public  interface ICSVUploader
    {
        public Task UploadScv<T, K>(IFormFile file) where T : class where K : ClassMap<T>;//K that extends ClassMap to make it generic
    }
}
