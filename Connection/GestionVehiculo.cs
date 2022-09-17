using EjercicioCanellaWEB.Models;
using Newtonsoft.Json;
using System.Text;

namespace EjercicioCanellaWEB.Connection
{
    public class GestionVehiculo
    {
        public IEnumerable<Vehiculo> GetVehiculos()
        {
            IEnumerable<Vehiculo> vehiculos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7212/api/v1/");
                var responseTask = client.GetAsync("vehiculo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Vehiculo>>();
                    readTask.Wait();
                    vehiculos = readTask.Result;
                }
                else
                {
                    vehiculos = Enumerable.Empty<Vehiculo>();   
                }
            }
            return vehiculos;
        }

        public bool NewVehiculo(Vehiculo vehiculo)
        {
            var json = JsonConvert.SerializeObject(vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7212/api/v1/");
                var responseTask = client.PostAsync("vehiculo", content);
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public Vehiculo GetVehiculo(string placa)
        {
            Vehiculo vehiculo = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7212/api/v1/");
                var responseTask = client.GetAsync(string.Format("auto?placa={0}", placa));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Vehiculo>();
                    readTask.Wait();
                    vehiculo = readTask.Result;
                }
                else
                {
                    vehiculo = new Vehiculo();
                }
            }
            return vehiculo;
        }

        public bool UpdateVehiculo(Vehiculo vehiculo)
        {
            var json = JsonConvert.SerializeObject(vehiculo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7212/api/v1/");
                var responseTask = client.PutAsync("vehiculo", content);
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public bool DeleteVehiculo(string placa)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7212/api/v1/");
                var responseTask = client.DeleteAsync(string.Format("vehiculo?placa={0}", placa));
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }
    }
}
