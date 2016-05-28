using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace NMCTS
{
    public class DeterministicNode:Node
    {
        public Node[] children;
        
        public Board board;

        public DeterministicNode(BoardState state,Move action)
        {
            this.board = new Board(state);
            this.action = action;
        }
        public void selectAction()
        {
            List<Node> visited = new List<Node>();
            Node cur = this; // root
            visited.Add(this);
            while (!(cur.isLeaf()))
            {
                cur = cur.select();
                // System.out.println("Adding: " + cur);
                visited.Add(cur);
                if (cur is NondeterministicNode)
                {
                    if (((NondeterministicNode)cur).selected != null)
                    {
                        cur = ((NondeterministicNode)cur).selected;
                    }
                }
            }

            cur.expand();
            Node newNode = cur.select();
            visited.Add(newNode);
            double value = newNode.rollOut(newNode);
            foreach (Node node in visited)
            {
                // would need extra logic for n-player game
                // System.out.println(node);
                node.updateStatus(value);
            }
        }

        public override void expand()
        {
            children = new Node[this.board.getCountActions()];
            List<Actions> tmp = this.board.getActions();
            if (tmp.Count == 0)
            {
                throw new InvalidOperationException("Tidak ada aksi yang dapat dilakukan");
            }
            int index = 0;
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].action == ACTION.MOVE)
                {
                    DeterministicActions action = (DeterministicActions)tmp[i];
                    int length = action.to.Count;
                    for (int x = 0; x < length; x++)
                    {
                        BoardState tmpBoardState = this.board.getBoardState();
                        this.board.move(action.from.row,action.from.column, action.to[x].row,action.to[x].column);
                        children[index] = new DeterministicNode(this.board.getBoardState(), new Move(action.from, action.to[x]));
                        this.board.restoreBoardState(tmpBoardState);
                        index++;
                    }
                }
                else
                {
                    NondeterministicActions action = (NondeterministicActions)tmp[i];
                    NondeterministicNode tmpNode = new NondeterministicNode(action.probability, new Move(action.position, action.position));

                    tmpNode.children = new DeterministicNode[action.probability.Count];
                    for (int x = 0; x <action.piece.Count; x++)
                    {
                        BoardState tmpBoardState = this.board.getBoardState();
                        Position tmpPosition = this.board.getFlippedPositionByPiece(action.piece[x]);//temukan real position
                        this.board.switchFlippedPieceByPosition(tmpPosition.row, tmpPosition.column,action.position.row,action.position.column);//pindahkan dari real position ke position yang diinginkan
                        this.board.flip(action.position.row, action.position.column); //buka piece
                        tmpNode.children[x] = new DeterministicNode(this.board.getBoardState(), new Move(action.position, action.position));
                        this.board.restoreBoardState(tmpBoardState);
                    }
                    children[index] = tmpNode;
                    index++;
                }
            }
        }

        public override Node select()
        {
            Node selected = null;
            double bestValue = double.MinValue;
            /*foreach (Node c in children)
            {
                double uctValue = c.winRate + Node.bias + Math.Sqrt(Math.Log(this.nVisits) / c.nVisits);
                // small random number to break ties randomly in unexpanded nodes
                // System.out.println("UCT value = " + uctValue);
                if (uctValue > bestValue)
                {
                    selected = c;
                    bestValue = uctValue;
                }
            }*/
                
                foreach (Node c in children)
                {
                    double uctValue =
                               c.winRate / (c.nVisits + epsilon) +
                                       Math.Sqrt(Math.Log(nVisits + 1) / (c.nVisits + epsilon)) +
                                       r.NextDouble() * epsilon;
                    // small random number to break ties randomly in unexpanded nodes
                    //Console.WriteLine("UCT value = " + uctValue);
                    if (uctValue > bestValue)
                    {
                        selected = c;
                        bestValue = uctValue;
                    }
                   
                }
            // System.out.println("Returning: " + selected);*/
            
            return selected;
        }

        public override double rollOut(Node tn)
        {
            DeterministicNode tmp = new DeterministicNode(((DeterministicNode)tn).board.getBoardState(),null);
           
            while (tmp.board.isEnd() == END_STATE.CONTINUE)
            {
                CESPFMove tmpMove = tmp.board.CESPF();
                if (tmpMove.isFlippingAction())
                {
                    tmp.board.flip(tmpMove.move.from.row, tmpMove.move.from.column);
                }
                else
                {
                    tmp.board.move(tmpMove.move.from.row, tmpMove.move.from.column, tmpMove.move.to.row, tmpMove.move.to.column);
                }
            }

            END_STATE end = tmp.board.isEnd();
            if (end == END_STATE.DRAW)
            {
                return 0.5;
            }
            else if (DeterministicNode.side == (int)end)
            {
                return 1;
            }

            return 0;
        }

        public override void updateStatus(double value)
        {
            this.winRate += value;
            this.nVisits += 1;
        }

        public override bool isLeaf()
        {
            return children == null;
        }
    }
}
