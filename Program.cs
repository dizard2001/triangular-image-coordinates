using System;
using Models;
using System.Collections.Generic;

namespace triangular_image_coordinates
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> rowCharacters = new List<char>();
            rowCharacters.Add('A');
            rowCharacters.Add('B');
            rowCharacters.Add('C');
            rowCharacters.Add('D');
            rowCharacters.Add('E');
            rowCharacters.Add('F');
            var triangleImageManager = new TriangleImageManager(rowCharacters, 12);

            var coordinatesForRowAColumn2 =
              triangleImageManager.GetTriangleCoordinatesForRowAndColumn('F', 11);
            Console.WriteLine(coordinatesForRowAColumn2.ToString());

            var v1 = new Coordinate(50, 60);
            var v2 = new Coordinate(50, 50);
            var v3 = new Coordinate(60,60);
            var RowAndColumnOfTriangleCoordinates =
              triangleImageManager.GetRowAndColumnForTriangleVertices(v1,v2,v3);
            Console.WriteLine(
              String.Format(
                "Row:{0} Column:{1}",
                RowAndColumnOfTriangleCoordinates.Item1,
                RowAndColumnOfTriangleCoordinates.Item2
            ));
        }
    }
}
