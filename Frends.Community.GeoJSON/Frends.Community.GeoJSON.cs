using System.ComponentModel;
using System.Threading;
using Microsoft.CSharp; // You can remove this if you don't need dynamic type in .NET Standard frends Tasks
using GeoJSON.Net.Feature;
using System.Collections.Generic;
using Position = GeoJSON.Net.Geometry.Position;
using LineString = GeoJSON.Net.Geometry.LineString;
using Polygon = GeoJSON.Net.Geometry.Polygon;

using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;
using System.Linq;

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
        var centroid = CalculateCentroid(polygon);
/*
        var centroidFeature = new Feature(centroid, new { name = "centroid" });

        var featureCollection = new FeatureCollection();
        featureCollection.Features.Add(new Feature(polygon));
        featureCollection.Features.Add(centroidFeature);

        string updatedGeoJson = JsonConvert.SerializeObject(featureCollection, Formatting.Indented);
*/
        var properties = new
        {
            centroid = new
            {
                latitude = centroid.Coordinates.Latitude,
                longitude = centroid.Coordinates.Longitude
            }
        };

        var polygonFeature = new Feature(polygon, properties);


        string updatedGeoJson = JsonConvert.SerializeObject(polygonFeature);


        var output = new Result
        {
            Ret = updatedGeoJson
        };

            return output;
    }
    

    private static Point CalculateCentroid(Polygon polygon)
    {
        var coordinates = polygon.Coordinates.First().Coordinates;
        double x = 0, y = 0;
        double signedArea = 0.0;
        double a = 0.0;  // Partial signed area

        for (int i = 0; i < coordinates.Count - 1; i++)
        {
            double x0 = coordinates[i].Longitude;
            double y0 = coordinates[i].Latitude;
            double x1 = coordinates[i + 1].Longitude;
            double y1 = coordinates[i + 1].Latitude;

            a = x0 * y1 - x1 * y0;
            signedArea += a;
            x += (x0 + x1) * a;
            y += (y0 + y1) * a;
        }

        signedArea *= 0.5;
        x /= (6.0 * signedArea);
        y /= (6.0 * signedArea);

        return new Point(new Position(y, x));
    }
}
}