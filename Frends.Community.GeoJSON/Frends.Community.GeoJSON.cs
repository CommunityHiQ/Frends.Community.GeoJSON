using System.ComponentModel;
using System.Threading;
using Microsoft.CSharp; // You can remove this if you don't need dynamic type in .NET Standard frends Tasks
using GeoJSON.Net.Feature;
using System.Collections.Generic;
using Position = GeoJSON.Net.Geometry.Position;
using LineString = GeoJSON.Net.Geometry.LineString;
using Polygon = GeoJSON.Net.Geometry.Polygon;

#pragma warning disable 1591

namespace Frends.Community.GeoJSON
{
    public static class GeoJson
    {
        /// <summary>
        /// This is task
        /// Documentation: https://github.com/CommunityHiQ/Frends.Community.GeoJSON
        /// </summary>
        /// <param name="input">What to repeat.</param>
        /// <param name="options">Define if repeated multiple times. </param>
        /// <param name="cancellationToken"></param>
        /// <returns>{string Replication} </returns>
        public static Result WKTPolygonToGeoJSON([PropertyTab] Parameters input, CancellationToken cancellationToken)
        {

        var reader = new NetTopologySuite.IO.WKTReader();
        var geometry = reader.Read(input.Wkt2);

        var coordinates = new List<Position>();
       
        foreach (var coord in geometry.Coordinates)
        {
            coordinates.Add(new Position(coord.Y, coord.X));
        }

        var polygon = new Polygon(new List<LineString> { new LineString(coordinates) });
        var feature = new Feature(polygon);

        var geoJson = Newtonsoft.Json.JsonConvert.SerializeObject(feature);

        var output = new Result
        {
            Ret = geoJson
        };
            return output;
        }
    }
}
