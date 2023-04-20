using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

using System.Drawing;
using System.Drawing.Imaging;
using OneMoreStepAPI.Models;

namespace OneMoreStepAPI
{
    public static class Util
    {
        

        public static MemoryStream Base64StringToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var resultStream = new MemoryStream();
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                ms.Write(imageBytes, 0, imageBytes.Length);
                resultStream = ms;
            }
            return resultStream;
        }

        public enum RouteSortOption
        {
            Likes,
            CreationDateTime
        }

        public enum SortOrder
        {
            Ascending,
            Descending
        }

        public static IQueryable<Route> SortRoutes(IQueryable<Route> routes, IQueryable<RoutesLikes> routesLikes, RouteSortOption sortOption, SortOrder sortOrder)
        {
            if (sortOption == RouteSortOption.Likes)
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    return routes.OrderBy(r => routesLikes.Count(rl => rl.RouteId == r.Id));
                }
                else
                {
                    return routes.OrderByDescending(r => routesLikes.Count(rl => rl.RouteId == r.Id));
                }
            }
            else // sortOption == RouteSortOption.CreationDateTime
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    return routes.OrderBy(r => r.CreationDateTime);
                }
                else
                {
                    return routes.OrderByDescending(r => r.CreationDateTime);
                }
            }
        }
    }
}
