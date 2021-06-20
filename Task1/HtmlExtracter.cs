using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Task1
{
    public class HtmlExtracter
    {
        public string CovertHTMLtoJSON()
        {
            StringBuilder pureText = new StringBuilder();
            HtmlDocument doc = new HtmlDocument();
            WebInfo info = new WebInfo();

            string fileName = "task 1 - Kempinski Hotel Bristol Berlin, Germany - Booking.com.html";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            doc.Load(path);

            info.HotelName = doc.GetElementbyId("hp_hotel_name").InnerText.Replace("\n", "").Replace("\r", "");

            info.Address = doc.GetElementbyId("hp_address_subtitle").InnerText.Replace("\n", "").Replace("\r", "");

            info.ClassificationStars = ExtactRating(doc.GetElementbyId("location_score_tooltip").InnerText.Replace("\n", "").Replace("\r", ""));

            info.ReviewPoints = doc.DocumentNode.SelectNodes("//div[@id='js--hp-gallery-scorecard']//span[@class='average js--hp-scorecard-scoreval']").FirstOrDefault().InnerText;

            info.NumberOfReviews = doc.DocumentNode.SelectNodes("//div[@id='js--hp-gallery-scorecard']//span[@class='trackit score_from_number_of_reviews']//strong").FirstOrDefault().InnerText;

            HtmlNodeCollection descriptionNodes = doc.DocumentNode.SelectNodes("//div[@class='hotel_description_wrapper_exp ']//p");

            info.Description = ConcatDescription(descriptionNodes);

            string categoriesHeading = doc.DocumentNode.SelectNodes("//div[@class='hp_last_booking']").FirstOrDefault().InnerText;

            string categoriesDetails = doc.GetElementbyId("maxotel_rooms").InnerText.Replace("\n", "").Replace("\r", "");

            info.RoomCategories = categoriesHeading + categoriesDetails;

            info.AlternativeHotels = doc.GetElementbyId("althotels-wrapper").InnerText.Replace("\n", "").Replace("\r", "");

            string jsonOutput = JsonConvert.SerializeObject(info);

            return jsonOutput;
        }

        private static string ExtactRating(String data)
        {
            string pattern = @"\d+\.*\d+/+\d+\.*\d*";
            Regex rg = new Regex(pattern);
            MatchCollection matchedRating = rg.Matches(data);
            String result = "Not found";
            for (int count = 0; count < matchedRating.Count; count++)
            {
                result = matchedRating[count].Value;
            }
            return result;
        }

        private static string ConcatDescription(HtmlNodeCollection descriptionNodes)
        {
            StringBuilder description = new StringBuilder();
            foreach (HtmlNode node in descriptionNodes)
            {
                description.Append(node.InnerText.Replace("\n", "").Replace("\r", ""));
            }

            return description.ToString();
        }

    }
}
