using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AstralTest.GeoLocation
{
    public class GeoService:IGeoService
    {
        /// <summary>
        /// Возвращает модель содержащию долготу и широту
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public async Task<LatLonModel> GetLatLon(string adress)
        {
            var urlAdress = $"https://geocode-maps.yandex.ru/1.x/?format=json&geocode=" + adress.Replace(' ', '+');
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, urlAdress);
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();
                var resultJson = JsonConvert.DeserializeObject<RootObject>(json);
                var getItem = resultJson.response.GeoObjectCollection.featureMember.FirstOrDefault();
                if (getItem == null)
                {
                    throw new NullReferenceException($"Адрес {adress} не найден.");
                }
                var points = getItem.GeoObject.Point.pos.Split(' ');
                return new LatLonModel {Latitude = points[1], Longitude = points[0]};
            }
        }

        /// <summary>
        /// Возвращает массив байтов содержащий картинку по данной  долготе и широте
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<byte[]> GetImage(LatLonModel model)
        {
            var points = $"{model.Longitude},{model.Latitude}";
            var urlAdress = $"https://static-maps.yandex.ru/1.x/?" +
                            $"ll={points}&l=map&size=450,450&z=16&pt={points},pmwtm1";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, urlAdress);
                var response = await client.SendAsync(request);
                var resultBytes = await response.Content.ReadAsByteArrayAsync();
                if (resultBytes.Length == 0)
                {
                    throw new NullReferenceException($"Не удалось получить изображение по данным {points} координатам.");
                }
                using (var stream = new FileStream("C:\\Users\\Alexander\\Desktop\\test\\test.jpg", FileMode.Create,
                    FileAccess.Write))
                {
                   await stream.WriteAsync(resultBytes, 0, resultBytes.Length);
                }
                    return null;
            }
        }
    }
}
