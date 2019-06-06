using GhostChess.Board.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostChess.Board.Core.Models
{
    public class Nodes : List<Node>
    {
        //TODO: v2 possibly implement methods as extensions to IEnumerable<T>
        //TODO: v2 reinforce to find by relative node names instead of fixed sizes

        private readonly BoardConfiguration _boardConfiguration;

        public Nodes(BoardConfiguration boardConfiguration)
        {
            _boardConfiguration = boardConfiguration;
        }

        public IEnumerable<Node> GetNodesAround(Node source)
        {
            List<Node> neighbouringNodes = new List<Node>();

            neighbouringNodes.Add(GetLeftNode(source));
            neighbouringNodes.Add(GetRightNode(source));
            neighbouringNodes.Add(GetUpperNode(source));
            neighbouringNodes.Add(GetLowerNode(source));
            neighbouringNodes.Add(GetUpperLeftNode(source));
            neighbouringNodes.Add(GetUpperRightNode(source));
            neighbouringNodes.Add(GetLowerLeftNode(source));
            neighbouringNodes.Add(GetLowerRightNode(source));

            neighbouringNodes.RemoveAll(t => t == null);
            return neighbouringNodes;
        }

        public IEnumerable<Node> GetRightCentralNodes()
        {
            Regex regex = new Regex(@"(^RA\d$|^RB\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public IEnumerable<Node> GetLeftCentralNodes()
        {
            Regex regex = new Regex(@"(^LA\d$|^LB\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public IEnumerable<Node> GetAllCentralNodes()
        {
            var regex = new Regex(@"^[A-H]\d$");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public Node GetLeftNode(Node origin) => this.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));

        public Node GetRightNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public Node GetUpperNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + _boardConfiguration.FieldSizeY));
        }

        public Node GetLowerNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - _boardConfiguration.FieldSizeY));
        }

        public Node GetUpperLeftNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        }

        public Node GetLowerLeftNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        }
        public Node GetUpperRightNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        }

        public Node GetLowerRightNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        }

        public IEnumerable<Node> GetLeftIntermediateBoundryNodes()
        {
            var regex = new Regex(@"(^LK\d$|^R\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public IEnumerable<Node> GetRightIntermediateBoundryNodes()
        {
            var regex = new Regex(@"(^I\d$|^RI\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public IEnumerable<Node> GetLeftCentralBoundryNodes()
        {
            var regex = new Regex(@"(^LB\d$|^H\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public IEnumerable<Node> GetRightCentralBoundryNodes()
        {
            var regex = new Regex(@"(^A\d$|^RA\d$)");
            return FindAll(n => regex.IsMatch(n.Name));
        }

        public Node GetLeftCentralBoundryNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.FieldSizeX - _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public Node GetRightCentralBoundryNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.FieldSizeX + _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public Node GetLeftIntermediateBoundryNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public Node GetRightIntermediateBoundryNode(Node origin)
        {
            return this.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.SideFieldOffsetX) && t.Y.Equals(origin.Y));
        }

        public IEnumerable<Node> GetSetUpNodes()
        {
            Regex regex = new Regex(@"[A-H]([1-2]|[7-8])");
            return FindAll(t => regex.Match(t.Name).Success);
        }

        public Node GetInitNode(IEnumerable<Node> nodes)
        {
            return nodes.FirstOrDefault(t => t.Name.Equals("LI0"));
        } 
    }
}
