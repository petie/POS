using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace POS.Tests.Integration
{
    public static class TestExtensions
    {
        public static async Task<T> GetResponse<T>(this HttpResponseMessage r) where T: class
        {
            var response = await r.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }
    }
}
