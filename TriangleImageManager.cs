using System.Collections.Generic;
using Models;
using System;

public class TriangleImageManager {
    public List<char> RowCharacters { get; private set; }
    public int NumberOfColumns { get; private set; }

    const int triangleWidth = 10;

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
            (column / 2) * triangleWidth :
            ((column -1) / 2) * triangleWidth;

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
            Boolean isTriangleOrientedBottomLeft = (i % 2) != 0;
            rowOfTriangles.Add(GetTriangleCoordinates(rowTopLeftCornerCoordinate, triangleWidth, isTriangleOrientedBottomLeft));
            
            if(!isTriangleOrientedBottomLeft) {
                rowTopLeftCornerCoordinate.X += triangleWidth;
            }
        }

        return rowOfTriangles;
    }

    public Tuple<char,int> GetRowAndColumnForTriangleVertices(List<Coordinate> triangleCoordinates) {
        throw new NotImplementedException("TODO");
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