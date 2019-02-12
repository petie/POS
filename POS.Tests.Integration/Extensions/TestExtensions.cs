using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using POS.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POS.Tests.Integration
{
    public static class TestExtensions
    {
        public static async Task<T> GetResponse<T>(this HttpResponseMessage r)
        {
            var response = await r.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return result;
        }

        public static async Task<Shift> CreateShift(this HttpClient c)
        {
            var httpResponse = await c.PostAsync("/api/shift", null);
            httpResponse.EnsureSuccessStatusCode();
            var shift = await httpResponse.GetResponse<Shift>();
            return shift;
        }

        public static async Task<Shift> GetCurrent(this HttpClient c)
        {
            var httpResponse = await c.GetAsync("/api/shift");
            httpResponse.EnsureSuccessStatusCode();
            var shift = await httpResponse.GetResponse<Shift>();
            return shift;
        }

        public static async Task<Shift> StartShift(this HttpClient c, int shiftId, decimal depositAmount)
        {
            var payload = new ShiftStartPayload(shiftId, depositAmount);
            var httpResponse = await c.PostAsJsonAsync("/api/shift/start", payload);
            httpResponse.EnsureSuccessStatusCode();
            var shift = await httpResponse.GetResponse<Shift>();
            return shift;
        }

        public static async Task<Shift> CloseShift(this HttpClient c)
        {
            var httpResponse = await c.PostAsync("/api/shift/close", null);
            httpResponse.EnsureSuccessStatusCode();
            var shift = await httpResponse.GetResponse<Shift>();
            return shift;
        }

        public static async Task<int> CreateReceipt(this HttpClient c)
        {
            var httpResponse = await c.PostAsync("/api/receipt", null);
            httpResponse.EnsureSuccessStatusCode();
            var receiptId = await httpResponse.GetResponse<int>();
            return receiptId;
        }

        public static async Task<ReceiptItem> AddToReceipt(this HttpClient c, string ean)
        {
            var httpResponse = await c.PostAsync($"/api/receipt/{ean}", null);
            httpResponse.EnsureSuccessStatusCode();
            var receiptId = await httpResponse.GetResponse<ReceiptItem>();
            return receiptId;
        }
    }
}
