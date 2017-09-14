using System.Collections.Generic;
using System;

namespace Models {
    public class Triangle {
        public Coordinate Vertex1 {get; private set;}
        public Coordinate Vertex2 {get; private set;}
        public Coordinate Vertex3 {get; private set;}

        public Triangle(Coordinate vertex1, Coordinate vertex2, Coordinate vertex3) {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }

        public override string ToString() {
            return String.Format("v1{0} v2{1} v3{2}",  Vertex1, Vertex2, Vertex3);
        }
    }
}