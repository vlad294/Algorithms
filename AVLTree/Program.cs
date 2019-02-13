using System;

namespace AVLTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tree = new BinaryTree();
            var random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                tree.Insert(random.Next(0, 1000));
            }
        }
    }

}
