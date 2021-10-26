using System;
using Graph;
using System.Collections.Generic;
using Automatonymous.Graphing;
using Cassandra.DataStax.Graph;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a list of vertices using the Vertex<T> class
            List<Vertex<string>> vertices = new List<Vertex<string>>
            (
                new Vertex<string>[]
                    {
                new Vertex<string>("Los Angeles"),
                new Vertex<string>("San Francisco"),
                new Vertex<string>("Las Vegas"),
                new Vertex<string>("Seattle"),
                new Vertex<string>("Austin"),
                new Vertex<string>("Portland")
                    }
            );

            // Establish edges; Ex. Los Angeles -> San Francisco, Las Vegas, Portland
            vertices[0].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[2], vertices[5]
            }));

            vertices[1].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[0], vertices[3], vertices[5]
            }));

            vertices[2].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[0], vertices[1], vertices[4]
            }));

            vertices[3].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[5]
            }));

            vertices[4].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[2]
            }));

            vertices[5].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[3]
            }));

            // Create graph using the UndirectedGenericGraph<T> class
            UndirectedGenericGraph<string> testGraph = new UndirectedGenericGraph<string>(vertices);

            // Check to see that all neighbors are properly set up
            foreach (Vertex<string> vertex in vertices)
            {
                Console.WriteLine(vertex.ToString());
            }

            // Test searching algorithms
            testGraph.DepthFirstSearch(vertices[0]);
            //testGraph.BreadthFirstSearch(vertices[0]);

        }
    }
}
