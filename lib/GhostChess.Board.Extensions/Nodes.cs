using GhostChess.Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GhostChess.Board.Extensions
{
    public class Nodes : List<Node>
    {
        private readonly BoardConfiguration _boardConfiguration;
        public Nodes()
        {

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

        public Node GetLeftNode(Node origin) => 
            this.FirstOrDefault(t => t.X.Equals(origin.X - _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));

        public Node GetRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + _boardConfiguration.FieldSizeX) && t.Y.Equals(origin.Y));
        }

        public Node GetUpperNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y + _boardConfiguration.FieldSizeY));
        }

        public Node GetLowerNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X) && t.Y.Equals(origin.Y - _boardConfiguration.FieldSizeY));
        }

        public Node GetUpperLeftNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        }

        public Node GetLowerLeftNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X - (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        }
        public Node GetUpperRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y + (_boardConfiguration.FieldSizeY / 2.0)));
        }

        public Node GetLowerRightNode(IEnumerable<Node> nodes, Node origin)
        {
            return nodes.FirstOrDefault(t => t.X.Equals(origin.X + (_boardConfiguration.FieldSizeX / 2.0)) && t.Y.Equals(origin.Y - (_boardConfiguration.FieldSizeY / 2.0)));
        }
    }
}
