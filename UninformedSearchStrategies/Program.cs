using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UninformedSearchStrategies;

namespace UninformedSearchStrategies
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter The Goal State");
            int Goal = int.Parse(Console.ReadLine());
            UnInformedSearch uis = new UnInformedSearch();
            uis.IDS(Goal);
            Console.ReadKey();
        }
    }

    class Node
    {
        public int State { get; set; }
        public int depth { get; set; }
        public int pathCost { get; set; }
        public Node parent { get; set; }

        // root 
        public Node()
        {
            this.State = 0;
            this.depth = 0;
            this.pathCost = 0;
            this.parent = null;
        }

        // root with changable state
        public Node(int State)
        {
            this.State = State;
            this.depth = 0;
            this.pathCost = 0;
            this.parent = null;
        }

        // children with constant cost
        public Node(Node parent, int State)
        {
            this.State = State;
            this.depth = parent.depth + 1;
            this.pathCost = parent.pathCost + 1;
            this.parent = parent;
        }

        // children with changable cost
        public Node(Node parent, int State, int stepCost)
        {
            this.State = State;
            this.depth = parent.depth + 1;
            this.pathCost = parent.pathCost + stepCost;
            this.parent = parent;
        }

        // add children
        public Node[] AddChildren()
        {
            Node[] Children = new Node[2];
            Children[0] = new Node(this, this.State * 2 + 1);
            Children[1] = new Node(this, this.State * 2 + 2);
            return Children;
        }

        // add children
        public Node[] AddChildrenReversed()
        {
            Node[] Children = new Node[2];
            Children[1] = new Node(this, this.State * 2 + 1);
            Children[0] = new Node(this, this.State * 2 + 2);
            return Children;
        }
    }
}

class UnInformedSearch
{
    public void BFS(int GoalState)
    {
        Queue<Node> BFS_Queue = new Queue<Node>();
        Node InitialState = new Node();
        BFS_Queue.Enqueue(InitialState);
        Console.WriteLine("Trace");
        while (BFS_Queue.Count != 0)
        {
            // remove
            Node CurrentNode = BFS_Queue.Dequeue();
            Console.Write(CurrentNode.State + "\t");
            //check goal
            if (CurrentNode.State == GoalState)
            {
                //return solution
                List<int> solution = new List<int>();
                while (CurrentNode != null)
                {
                    solution.Insert(0, CurrentNode.State);
                    CurrentNode = CurrentNode.parent;
                }
                Console.WriteLine();
                Console.WriteLine("Solution");
                foreach (var item in solution)
                {
                    Console.Write(item + "\t");
                }
                break;
            }
            //add children
            foreach (var item in CurrentNode.AddChildren())
            {
                BFS_Queue.Enqueue(item);
            }
        }
    }

    public void DFS(int GoalState)
    {
        Stack<Node> DFS_Stack = new Stack<Node>();
        Node InitialState = new Node();
        DFS_Stack.Push(InitialState);
        Console.WriteLine("Trace");
        while (DFS_Stack.Count != 0)
        {
            // remove
            Node CurrentNode = DFS_Stack.Pop();
            Console.Write(CurrentNode.State + "\t");
            //check goal
            if (CurrentNode.State == GoalState)
            {
                //return solution
                List<int> solution = new List<int>();
                while (CurrentNode != null)
                {
                    solution.Insert(0, CurrentNode.State);
                    CurrentNode = CurrentNode.parent;
                }
                Console.WriteLine();
                Console.WriteLine("Solution");
                foreach (var item in solution)
                {
                    Console.Write(item + "\t");
                }
                break;
            }
            //add children
            foreach (var item in CurrentNode.AddChildrenReversed())
            {
                DFS_Stack.Push(item);
            }
        }
    }

    public bool DLS(int GoalState, int Limit)
    {
        Stack<Node> DLS_Stack = new Stack<Node>();
        Node InitialState = new Node();
        DLS_Stack.Push(InitialState);
        Console.WriteLine("Trace");
        while (DLS_Stack.Count != 0)
        {
            // remove
            Node CurrentNode = DLS_Stack.Pop();
            Console.Write(CurrentNode.State + "\t");
            //check goal
            if (CurrentNode.State == GoalState)
            {
                //return solution
                List<int> solution = new List<int>();
                while (CurrentNode != null)
                {
                    solution.Insert(0, CurrentNode.State);
                    CurrentNode = CurrentNode.parent;
                }
                Console.WriteLine();
                Console.WriteLine("Solution");
                foreach (var item in solution)
                {
                    Console.Write(item + "\t");
                }
                return true;
            }
            //add children
            if (CurrentNode.depth == Limit)
            {
                continue;
            }
            foreach (var item in CurrentNode.AddChildrenReversed())
            {
                DLS_Stack.Push(item);
            }
        }
        return false;
    }

    public void IDS(int GoalState)
    {
        int Limit = 0;
        while (DLS(GoalState, Limit) == false)
        {
            Limit++;
        }
    }
}


