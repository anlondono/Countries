using Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Services
{
    public interface IApiService
    {

        Task<Response> GetList<T>(string url, string controller);
        
    }
}
