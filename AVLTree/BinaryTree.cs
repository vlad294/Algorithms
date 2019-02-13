namespace AVLTree
{
    public class BinaryTree
    {
        private Node _root;

        public void Insert(int key)
        {
            if (_root == null)
            {
                _root = new Node { Key = key };
                return;
            }
            InsertInternal(_root, key);
        }

        private Node InsertInternal(Node node, int key)
        {
            if (node == null) return new Node { Key = key };
            if (key < node.Key)
            {
                node.Left = InsertInternal(node.Left, key);
            }
            else
            {
                node.Right = InsertInternal(node.Right, key);
            }

            return Balance(node);
        }

        private Node Balance(Node node)
        {
            InitHeight(node);

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
            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            InitHeight(node);
            InitHeight(left);

            return left;
        }

        private Node RotateLeft(Node node)
        {
            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            InitHeight(node);
            InitHeight(right);

            return right;
        }

        private void InitHeight(Node node)
        {
            var right = node.RightHeight;
            var left = node.LeftHeight;
            node.Height = (byte)((right > left ? right : left) + 1);
        }

        internal class Node
        {
            public int Key;
            public byte Height;
            public Node Left;
            public Node Right;
            public byte LeftHeight => Left?.Height ?? 0;
            public byte RightHeight => Right?.Height ?? 0;
            public int BalanceFactor => RightHeight - LeftHeight;
        }
    }

}
