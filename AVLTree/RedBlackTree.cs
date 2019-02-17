using System.Collections.Generic;

namespace AVLTree
{
    public class RedBlackTree<T>
    {
        private Node root;
        private readonly IComparer<T> _comparer;

        public RedBlackTree()
        {
            _comparer = Comparer<T>.Default;
        }

        public RedBlackTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public void Insert(T item)
        {
            var newNode = new Node(item);
            InsertInternal(newNode);
            AdjustTreeAfterInsertion(newNode);
        }

        private Node InsertInternal(Node newNode)
        {
            if (root == null)
            {
                root = newNode;
                return newNode;
            }

            if (newNode.Parent == null)
            {
                newNode.Parent = root;
            }

            if (_comparer.Compare(newNode.Item, newNode.Parent.Item) < 0)
            {
                if (newNode.Parent.Left == null)
                {
                    newNode.Parent.Left = newNode;
                    return newNode;
                }

                newNode.Parent = newNode.Parent.Left;
                newNode.Parent.Left = InsertInternal(newNode);
            }
            else
            {
                if (newNode.Parent.Right == null)
                {
                    newNode.Parent.Right = newNode;
                    return newNode;
                }

                newNode.Parent = newNode.Parent.Right;
                newNode.Parent.Right = InsertInternal(newNode);
            }

            return newNode;
        }

        private Node AdjustTreeAfterInsertion(Node node)
        {
            // STEP 0
            // The current node is at the root of the tree
            if (node == root)
            {
                node.Color = NodeColor.Black;
                return node;
            }

            // STEP 1
            // Color the current node as red
            node.Color = NodeColor.Red;

            // STEP 2
            // Tree is still valid, no adjustment needed
            if (node.Parent.Color == NodeColor.Black)
            {
                return node;
            }

            // STEP 3
            // Fix the double red-consecutive-nodes problems
            // Parent and uncle is red (newNode is also red)
            var grandparent = GetGrandparent(node);
            var uncle = GetUncle(node);
            if (uncle != null && uncle.Color == NodeColor.Red)
            {
                // simply recolor
                node.Parent.Color = NodeColor.Black;
                uncle.Color = NodeColor.Black;
                grandparent.Color = NodeColor.Red;

                // bubble up to see if more work is needed
                return AdjustTreeAfterInsertion(grandparent);
            }

            // STEP 4
            // TODO write description
            // and is right child
            if (node.Parent == grandparent.Left && node == node.Parent.Right)
            {
                RotateLeft(node.Parent);
            }
            // and is left child
            else if (node.Parent == grandparent.Right && node == node.Parent.Left)
            {
                RotateRight(node.Parent);
            }

            // STEP 5
            // TODO write description
            // Color parent as black
            node.Parent.Color = NodeColor.Black;

            // Color grandparent as red
            grandparent.Color = NodeColor.Red;

            if (node == node.Parent.Left && node.Parent == grandparent.Left)
            {
                RotateRight(grandparent);
            }
            // if (node == node.Parent.Right && node.Parent == grandparent.Right)
            else
            {
                RotateLeft(grandparent);
            }

            return node;
        }

        private Node GetUncle(Node node)
        {
            var grandParent = GetGrandparent(node);
            if (grandParent == null)
                return null;

            return node.Parent == grandParent.Left 
                ? grandParent.Right 
                : grandParent.Left;

        }

        private Node GetGrandparent(Node node) => node.Parent?.Parent;

        private Node RotateLeft(Node node)
        {
            if (node.Right == null)
            {
                return node;
            }

            // Pivot on right child
            var pivotNode = node.Right;

            // Check if currentNode is it's parent's left child.
            bool isLeftChild = node.Parent.Left == node;

            // Check if currentNode is the Root
            bool isRootNode = node == root;

            // Perform the rotation
            node.Right = pivotNode.Left;
            pivotNode.Left = node;

            // Update parents references
            node.Parent = pivotNode;
            pivotNode.Parent = node.Parent;

            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            //Update the entire tree's Root if necessary
            if (isRootNode)
            {
                root = pivotNode;
            }

            // update the original parent's child node
            if (node.Parent != null)
            {
                if (isLeftChild)
                {
                    node.Parent.Left = pivotNode;
                }
                else
                {
                    node.Parent.Right = pivotNode;
                }
            }

            return node;
        }

        private Node RotateRight(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            // Pivot on left child
            var pivotNode = node.Left;

            // Check if currentNode is it's parent's left child.
            bool isLeftChild = node.Parent.Left == node;

            // Check if currentNode is the Root
            bool isRootNode = node == root;

            // Perform the rotation
            node.Left = pivotNode.Right;
            pivotNode.Right = node;

            // Update parents references
            node.Parent = pivotNode;
            pivotNode.Parent = node.Parent;

            if (node.Left != null)
            {
                node.Left.Parent = node;
            }

            //Update the entire tree's Root if necessary
            if (isRootNode)
            {
                root = pivotNode;
            }

            // update the original parent's child node
            if (node.Parent != null)
            {
                if (isLeftChild)
                {
                    node.Parent.Left = pivotNode;
                }
                else
                {
                    node.Parent.Right = pivotNode;
                }
            }

            return node;
        }

        internal class Node
        {
            public Node Left;
            public Node Right;
            public Node Parent;
            public NodeColor Color;
            public T Item;

            public Node(T item)
            {
                Item = item;
            }
        }

        internal enum NodeColor: byte
        {
            Red,
            Black
        }
    }
}
