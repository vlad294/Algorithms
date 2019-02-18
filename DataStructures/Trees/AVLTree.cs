using System.Collections.Generic;

namespace DataStructures.Trees
{
    public class AVLTree<T>
    {
        private Node _root;
        private readonly IComparer<T> _comparer;

        public AVLTree()
        {
            _comparer = Comparer<T>.Default;
        }

        public AVLTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public void Insert(T item)
        {
            if (_root == null)
            {
                _root = new Node(item);
                return;
            }
            InsertInternal(_root, item);
        }

        private Node InsertInternal(Node parentNode, T item)
        {
            if (parentNode == null) return new Node(item);
            
            // if item less when node.Item
            if (_comparer.Compare(item, parentNode.Item) < 0)
            {
                parentNode.Left = InsertInternal(parentNode.Left, item);
            }
            else
            {
                parentNode.Right = InsertInternal(parentNode.Right, item);
            }

            return Balance(parentNode);
        }

        private Node Balance(Node node)
        {
            CorrectHeight(node);

            if (node.BalanceFactor == 2)
            {
                if (node.Right.BalanceFactor < 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                return RotateLeft(node);
            }

            if (node.BalanceFactor == -2)
            {
                if (node.Left.BalanceFactor > 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                return RotateRight(node);
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            var pivotNode = node.Left;
            node.Left = pivotNode.Right;
            pivotNode.Right = node;

            CorrectHeight(node);
            CorrectHeight(pivotNode);

            if (node == _root)
            {
                _root = pivotNode;
            }

            return pivotNode;
        }

        private Node RotateLeft(Node node)
        {
            var pivotNode = node.Right;
            node.Right = pivotNode.Left;
            pivotNode.Left = node;

            CorrectHeight(node);
            CorrectHeight(pivotNode);

            if (node == _root)
            {
                _root = pivotNode;
            }

            return pivotNode;
        }

        /// <summary>
        /// Corrects the height of a node
        /// Height = 1 + the max between left and right children.
        /// </summary>
        /// <param name="node"></param>
        private void CorrectHeight(Node node)
        {
            var right = node.RightHeight;
            var left = node.LeftHeight;
            node.Height = (byte)((right > left ? right : left) + 1);
        }

        internal class Node
        {
            public byte Height;
            public Node Left;
            public Node Right;
            public T Item;
            public byte LeftHeight => Left?.Height ?? 0;
            public byte RightHeight => Right?.Height ?? 0;
            public int BalanceFactor => RightHeight - LeftHeight;

            public Node(T item)
            {
                Item = item;
            }
        }
    }

}
