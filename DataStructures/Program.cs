﻿using System;
using DataStructures.Trees;

namespace DataStructures
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tree = new RedBlackTree<int>();
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                tree.Insert(random.Next(0, 100));
            }
        }
    }

}
