using Banisi.Application.Shared.Contracts.Infrastructure;
using Banisi.Application.Shared.Models.Base;
using Banisi.Infrastructure.Shared.Resources;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

namespace Banisi.Infrastructure.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;

        public ApiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> GetAsync<T>(string url, Dictionary<string, string> headers = null) where T : class
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    serviceResponse.SetFaultyState(response.StatusCode, Resource.WasProblemWithTheRequestMessage);
                    return serviceResponse;
                }

                string content = await response.Content.ReadAsStringAsync();

                if (content != string.Empty)
                    serviceResponse.Data = JsonSerializer.Deserialize<T>(content);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex.Message);
                return serviceResponse;
            }
        }

        //public async Task<ServiceResponse> PostAsync<T>(string url, T data) where T : class
        //{
        //    var serviceResponse = new ServiceResponse();

        //    try
        //    {
        //        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        //        var response = await _httpClient.PostAsync(url, content);

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            serviceResponse.SetFaultyState(response.StatusCode, Resource.WasProblemWithTheRequestMessage);
        //            return serviceResponse;
        //        }

        //        string responseObject = await response.Content.ReadAsStringAsync();

        //        if (responseObject != string.Empty)
        //            serviceResponse.Data = JsonSerializer.Deserialize<T>(responseObject);

        //        return serviceResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex.Message);
        //        return serviceResponse;
        //    }
        //}


        public async Task<ServiceResponse> PostAsync<T>(string url, T data, Dictionary<string, string> headers = null) where T : class
        {
            var serviceResponse = new ServiceResponse();

            #region Certificado SSL

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.Add(header.Key, header.Value);
                    }
                }

                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    serviceResponse.SetFaultyState(response.StatusCode, Resource.WasProblemWithTheRequestMessage);
                    return serviceResponse;
                }

                string responseObject = await response.Content.ReadAsStringAsync();

                if (responseObject != string.Empty)
                    serviceResponse.Data = JsonSerializer.Deserialize<T>(responseObject);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex.Message);
                return serviceResponse;
            }

            #endregion

        }

        public async Task<ServiceResponse> PutAsync<T>(string url, T data) where T : class
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    serviceResponse.SetFaultyState(response.StatusCode, Resource.WasProblemWithTheRequestMessage);
                    return serviceResponse;
                }

                string responseObject = await response.Content.ReadAsStringAsync();

                if (responseObject != string.Empty)
                    serviceResponse.Data = JsonSerializer.Deserialize<T>(responseObject);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex.Message);
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse> DeleteAsync<T>(string url)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    serviceResponse.SetFaultyState(response.StatusCode, Resource.WasProblemWithTheRequestMessage);
                    return serviceResponse;
                }

                string responseObject = await response.Content.ReadAsStringAsync();

                if (responseObject != string.Empty)
                    serviceResponse.Data = JsonSerializer.Deserialize<T>(responseObject);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.SetFaultyState(HttpStatusCode.InternalServerError, ex.Message);
                return serviceResponse;
            }
        }
    }
}
