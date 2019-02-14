using System;

namespace AVLTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tree = new AVLTree<int>();
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                tree.Insert(random.Next(0, 100));
            }
        }
    }

}
