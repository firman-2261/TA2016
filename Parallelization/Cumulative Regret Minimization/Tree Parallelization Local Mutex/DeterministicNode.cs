using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Engine;

namespace Parallelization.CR.Tree
{
    public class DeterministicNode : Node
    {
        public Node[] children { set; get; }

        public Board board;
        private int isLock;
        public int s;

        public DeterministicNode(BoardState state, Move action, int piece, double probability)
        {
            this.board = new Board(state);
            this.action = action;
            this.isLock = Available;
            this.name = Constant.DETERMINISTIC_NODE;
            this.piece = piece;
            this.probability = probability;
        }
        public void selectAction()
        {
            List<Node> visited = new List<Node>();
            Node cur = this; // root
            visited.Add(cur);
            while (!(cur.isLeaf()))
            {
                cur = cur.select();
                visited.Add(cur);
                if (cur is NondeterministicNode)
                {
                    cur = ((NondeterministicNode)cur.select()).selected;
                    visited.Add(cur);
                }
            }

            cur.expand();
            double value = cur.rollOut();
            int panjangSimulasi = (cur as DeterministicNode).s;
            foreach (Node node in visited)
            {
                node.updateStatus(value, panjangSimulasi);
            }

            Interlocked.CompareExchange(ref ((DeterministicNode)cur).isLock, Available, Taken);

        }

        public override Node select()
        {
            Node selected = null;
            double bestUCB = -(double.MaxValue);
            double ucb = 0;
            foreach (Node c in children)
            {
                if (c.nVisits == 0)
                {
                    ucb = double.MaxValue;
                }
                else
                {
                    if (c is NondeterministicNode)
                    {
                        if (((NondeterministicNode)c).selected.nVisits == 0)
                        {
                            ucb = double.MaxValue;
                        }
                        else
                        {
                            ucb = ((NondeterministicNode)c).selected.winRate + bias * (Math.Sqrt((Math.Log(this.nVisits)) / ((NondeterministicNode)c).selected.nVisits));
                        }
                    }
                    else
                    {
                        ucb = c.winRate + bias * (Math.Sqrt((Math.Log(this.nVisits)) / c.nVisits));
                    }
                }

                if (bestUCB <= ucb)
                {
                    bestUCB = ucb;
                    selected = c;
                }
            }
            return selected;
        }

        public override void expand()
        {
            int actionLength = this.board.getCountActions();
            if (actionLength != 0)
            {
                children = new Node[actionLength];

                List<Actions> tmp = this.board.getActions();
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
                            this.board.move(action.from.row, action.from.column, action.to[x].row, action.to[x].column);
                            children[index] = new DeterministicNode(this.board.getBoardState(), new Move(action.from, action.to[x]), Constant.NONE, Constant.NONE);

                            this.board.restoreBoardState(tmpBoardState);
                            index++;
                        }
                    }
                    else
                    {
                        NondeterministicActions action = (NondeterministicActions)tmp[i];
                        NondeterministicNode tmpNode = new NondeterministicNode(action.probability, new Move(action.position, action.position));

                        tmpNode.children = new DeterministicNode[action.probability.Count];
                        for (int x = 0; x < action.piece.Count; x++)
                        {
                            BoardState tmpBoardState = this.board.getBoardState();
                            Position tmpPosition = this.board.getFlippedPositionByPiece(action.piece[x]);//temukan real position
                            this.board.switchFlippedPieceByPosition(tmpPosition.row, tmpPosition.column, action.position.row, action.position.column);//pindahkan dari real position ke position yang diinginkan
                            this.board.flip(action.position.row, action.position.column); //buka piece
                            tmpNode.children[x] = new DeterministicNode(this.board.getBoardState(), new Move(action.position, action.position), action.piece[x], action.probability[x]);
                            this.board.restoreBoardState(tmpBoardState);
                        }
                        children[index] = tmpNode;
                        index++;
                    }
                }
            }
        }
        public override double rollOut()
        {
            this.s = 0;
            DeterministicNode tmp = new DeterministicNode(this.board.getBoardState(), null, Constant.NONE, Constant.NONE);

            while (tmp.board.isEnd() == END_STATE.CONTINUE)
            {
                this.s += 1;
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

        public override void updateStatus(double value,int s)
        {
            if (value == 1)
            {
                this.winRate = this.winRate + value + d * (PGL - s);
            }
            else
            {
                this.winRate += value;
            }
            this.nVisits += 1;
        }

        public override bool isLeaf()
        {
            lock (_lock)
            {
                while (this.isLock == Taken)
                {
                    Thread.SpinWait(1);
                }
                if (children == null)
                {
                    Interlocked.CompareExchange(ref this.isLock, Taken, Available);
                }
            }
            return children == null;
        }

    }
}
