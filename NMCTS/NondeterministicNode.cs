﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace NMCTS
{
    public class NondeterministicNode:Node
    {
        public List<double> dp;
        public Node selected;
        public Node[] children;

        public NondeterministicNode(List<double> dp,Move action)
        {
            this.dp = dp;
            this.action = action;
        }

        public override Node select()
        {/*
            foreach (double x in dp)
            {
                Console.WriteLine(x);
            }
            int index = Shuffle.rouletteSelect(this.dp);*/
            //Console.WriteLine(index);
           // Console.WriteLine(index);
            selected = children[Shuffle.rouletteSelect(this.dp)];
            return this;
        }

        public override void expand()
        {
            DeterministicNode tmpSelected = (DeterministicNode)selected;
            tmpSelected.children = new Node[tmpSelected.board.getCountActions()];
            List<Actions> tmp = tmpSelected.board.getActions();

            if (tmp.Count == 0)
            {
                throw new InvalidOperationException("Tidak ada aksi yang dapat dilakukan");
            }

            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].action == ACTION.MOVE)
                {
                    DeterministicActions action = (DeterministicActions)tmp[i];
                    int length = action.to.Count;
                    for (int x = 0; x < length; x++)
                    {
                        BoardState tmpBoardState = tmpSelected.board.getBoardState();
                        tmpSelected.board.move(action.from.row, action.from.column, action.to[x].row, action.to[x].column);
                        tmpSelected.children[i] = new DeterministicNode(tmpSelected.board.getBoardState(), new Move(action.from, action.to[x]));
                        tmpSelected.board.restoreBoardState(tmpBoardState);
                    }
                }
                else
                {
                    NondeterministicActions action = (NondeterministicActions)tmp[i];
                    NondeterministicNode tmpNode = new NondeterministicNode(action.probability, new Move(action.position, action.position));
                    tmpNode.children = new DeterministicNode[action.probability.Count];
                    for (int x = 0; x < action.piece.Count; x++)
                    {
                        BoardState tmpBoardState = tmpSelected.board.getBoardState();
                        Position tmpPosition = tmpSelected.board.getFlippedPositionByPiece(action.piece[x]);//temukan real position
                        tmpSelected.board.switchFlippedPieceByPosition(tmpPosition.row, tmpPosition.column, action.position.row, action.position.column);//pindahkan dari real position ke position yang diinginkan
                        tmpSelected.board.flip(action.position.row, action.position.column); //buka piece
                        tmpNode.children[x] = new DeterministicNode(tmpSelected.board.getBoardState(), new Move(action.position, action.position));
                        tmpSelected.board.restoreBoardState(tmpBoardState);
                    }
                    tmpSelected.children[i] = tmpNode;
                }
            }
        }

        public override double rollOut(Node tn)
        {
            if (((NondeterministicNode)tn).selected == null)
            {
                tn = select();
                
            }
            DeterministicNode tmp = new DeterministicNode(((DeterministicNode)((NondeterministicNode)tn).selected).board.getBoardState(),null);
            
            while (tmp.board.isEnd() == END_STATE.CONTINUE)
            {
                CESPFMove tmpMove = tmp.board.CESPF();
                //Console.WriteLine("sebelum" + tmp.board.sideToMove +" " +move.ToString());
                //tmp.board.printArray();
                //tmp.board.printBitboardBinaryString();
                if (tmpMove.isFlippingAction())
                {
                    tmp.board.flip(tmpMove.move.from.row, tmpMove.move.from.column);
                }
                else
                {
                    tmp.board.move(tmpMove.move.from.row, tmpMove.move.from.column, tmpMove.move.to.row, tmpMove.move.to.column);
                }
                /*Console.WriteLine("setelah" + tmp.board.sideToMove + " "+ move.ToString());
                tmp.board.printArray();
                tmp.board.printBitboardBinaryString();*/
            }

            END_STATE end = tmp.board.isEnd();
            //Console.WriteLine((int)end);
            //Console.WriteLine(side);
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
            this.selected.nVisits += 1;
            this.selected.winRate += value;
            this.nVisits += 1;
            this.winRate += value;
        }
        public override bool isLeaf()
        {
            /*if (selected == null)
            {
                return children == null;
            }
            else
            {
                /*if (((DeterministicNode)selected).children !=null)
                {
                    Console.WriteLine("hallo");
                    Console.WriteLine(((DeterministicNode)selected).children.Length);
                }*/
              //  return ((DeterministicNode)selected).children == null;
            //}
            return children == null;
        }

    }
}
