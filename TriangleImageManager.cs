using System.Collections.Generic;
using Models;
using System;

public class TriangleImageManager {
    public List<char> RowCharacters { get; private set; }
    public int NumberOfColumns { get; private set; }

    const int triangleWidth = 10;
    const int COLUMNS_PER_WIDTH = 2;
    const string VERTEX_NOT_IN_BOUNDS = "Vertex not in bounds";

    public TriangleImageManager(List<char> rowCharacters, int numberOfColumns) {
        if(numberOfColumns <= 0) {
            throw new ArgumentException("should be greater than 0", "numberOfColumns");
        }
        RowCharacters = rowCharacters;
        NumberOfColumns = numberOfColumns;
    }

    public Triangle GetTriangleCoordinatesForRowAndColumn(char row, int column) {
        var rowIndex = RowCharacters.IndexOf(row);
        if(rowIndex == -1) {
            throw new ArgumentException("The row character specified does not exist", "row");
        }

        if(column > NumberOfColumns || column < 1) {
            throw new ArgumentException(String.Format("The column specified must be between 1 - {0}", NumberOfColumns), "column");
        }

        // if column is even, it's a BottomLeft triangle
        // if column is odd, it's a topRightTriangle
        Boolean isTriangleOrientedBottomLeft = (column % 2) != 0;
        var startingXCoordinate = isTriangleOrientedBottomLeft ?
            (column / COLUMNS_PER_WIDTH) * triangleWidth :
            ((column -1) / COLUMNS_PER_WIDTH) * triangleWidth;

        var TopLeftCornerCoordinate = new Coordinate(startingXCoordinate, rowIndex * triangleWidth);

        return GetTriangleCoordinates(TopLeftCornerCoordinate, triangleWidth, isTriangleOrientedBottomLeft);
    }

    public List<Triangle> GetTriangleCoordinatesForRowUpToColumn(char row, int column) {
        var rowIndex = RowCharacters.IndexOf(row);
        if(rowIndex == -1) {
            throw new ArgumentException("The row character specified does not exist", "row");
        }

        if(column > NumberOfColumns || column < 1) {
            throw new ArgumentException(String.Format("The column specified must be between 1 - {0}", NumberOfColumns), "column");
        }

        var rowOfTriangles = new List<Triangle>();
        var rowTopLeftCornerCoordinate = new Coordinate(0, rowIndex * triangleWidth);

        for(int i = 1; i <= column; i++ ) {
            // if column is even, it's a BottomLeft triangle
            // if column is odd, it's a topRightTriangle
            Boolean isTriangleOrientedBottomLeft = (i % COLUMNS_PER_WIDTH) != 0;
            rowOfTriangles.Add(GetTriangleCoordinates(rowTopLeftCornerCoordinate, triangleWidth, isTriangleOrientedBottomLeft));

            if(!isTriangleOrientedBottomLeft) {
                rowTopLeftCornerCoordinate.X += triangleWidth;
            }
        }

        return rowOfTriangles;
    }

    public Tuple<char,int> GetRowAndColumnForTriangleVertices(Coordinate v1, Coordinate v2, Coordinate v3) {
        var maxX = NumberOfColumns * triangleWidth;
        var maxY = RowCharacters.Count * triangleWidth;

        if(!isCoordinateInBounds(v1, maxX, maxY)) {
          throw new ArgumentException(VERTEX_NOT_IN_BOUNDS, "v1");
        }
        if(!isCoordinateInBounds(v2, maxX, maxY)) {
          throw new ArgumentException(VERTEX_NOT_IN_BOUNDS, "v2");
        }
        if(!isCoordinateInBounds(v3, maxX, maxY)) {
          throw new ArgumentException(VERTEX_NOT_IN_BOUNDS, "v3");
        }

        // Could use additional validation to make sure coordinates are valid triangles.
        // v2 coordinate should coorespond to top left corner of (square) essentially
        var rowIndex = v2.Y / triangleWidth;

        var isBottomLeftTriangle = v2.X == v1.X;
        var baseColumnIndex = v2.X / (triangleWidth / COLUMNS_PER_WIDTH);
        var columnIndex = baseColumnIndex + (isBottomLeftTriangle ? 1 : 2);

        return new Tuple<char,int>(RowCharacters[rowIndex], columnIndex);
    }

    private Boolean isCoordinateInBounds(Coordinate coordinate, int MaxX, int MaxY) {
      return coordinate.X >= 0 && coordinate.X <= MaxX && coordinate.Y >= 0 && coordinate.Y <= MaxY;
    }

    private Triangle GetTriangleCoordinates(Coordinate topLeftCoordinate, int width, Boolean isOrientedBottomLeft) {
        var vertex1 = isOrientedBottomLeft ?
            new Coordinate(topLeftCoordinate.X, topLeftCoordinate.Y + width) :
            new Coordinate(topLeftCoordinate.X + width, topLeftCoordinate.Y);
        var vertex2 = new Coordinate(topLeftCoordinate.X, topLeftCoordinate.Y);
        var vertex3 = new Coordinate(topLeftCoordinate.X + width, topLeftCoordinate.Y + width);
        return new Triangle(vertex1, vertex2, vertex3);
    }
 }
