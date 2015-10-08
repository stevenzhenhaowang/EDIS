using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace EDISAngular.Services
{
    public class GoogleGeoService
    {
        private const string apiKey = "AIzaSyCEcmhjJDqbd0-LT22yZHd0yFeqbjFNZjc";



        /// <summary>
        /// returns latitude 
        /// </summary>
        /// <param name="addresspoint"></param>
        /// <returns></returns>
        public static double? GetCoordinatesLat(string addresspoint)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string seachurl = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + addresspoint + "&key=" + apiKey;
                    //string[] geocodeInfo = client.DownloadString(seachurl).Split(',');
                    //return (Convert.ToDouble(geocodeInfo[2]));
                    var result = client.DownloadString(seachurl);
                    XDocument document = XDocument.Parse(result);
                    var value = document.Descendants("location").First().Element("lat").Value;
                    return (Convert.ToDouble(value));
                }
                catch (Exception)
                {

                    return null;
                }



            }
        }

        /// <summary>
        /// returns longitude 
        /// </summary>
        /// <param name="addresspoint"></param>
        /// <returns></returns>
        public static double? GetCoordinatesLng(string addresspoint)
        {
            using (var client = new WebClient())
            {
                try
                {
                    string seachurl = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + addresspoint + "&key=" + apiKey;
                    var result = client.DownloadString(seachurl);
                    XDocument document = XDocument.Parse(result);
                    var value = document.Descendants("location").First().Element("lng").Value;
                    return (Convert.ToDouble(value));
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
    }

}