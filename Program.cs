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

            var coordinatesForRowAColumn2 = triangleImageManager.GetTriangleCoordinatesForRowAndColumn('F', 12);
            Console.WriteLine(coordinatesForRowAColumn2.ToString());
        }
    }
}
