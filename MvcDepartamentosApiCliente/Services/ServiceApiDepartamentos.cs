using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcDepartamentosApiCliente.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcDepartamentosApiCliente.Services
{
    public class ServiceApiDepartamentos
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDepartamentos(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "/api/departamentos";
            List<Departamento> departamentos =
                await this.CallApiAsync<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int iddepartamento)
        {
            string request = "/api/departamentos/" + iddepartamento;
            Departamento departamento =
                await this.CallApiAsync<Departamento>(request);
            return departamento;
        }

        public async Task DeleteDepartamentoAsync(int iddepartamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + iddepartamento;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                string json = JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR OBJETOS AL SERVIDOR SE UTILIZA
                //LA CLASE StringContent QUE CONTIENE EL OBJETO (data)
                //Y LA CODIFICACION DEL OBJETO
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre
            , string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Departamento departamento = new Departamento
                { IdDepartamento = id, Nombre = nombre, Localidad = localidad };
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }
    }
}
