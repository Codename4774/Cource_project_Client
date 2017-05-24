using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bomberman_client.GameClasses;
using System.Drawing;

namespace Bomberman_client
{
    public class MessageAnalyzer
    {
        GameClasses.GameCore gameCore;
        public enum KindMessage { Player = 0, Wall = 1 };
        public enum KindPlayerMessages { NewDirection = 0, Spawn = 1, Death = 2, PlaceBomb = 3, Connect = 4, Disconnect = 5, Location = 6, StopWalking = 7 };
        public int GetNextValue(byte[] message, ref int i)
        {
            int result = BitConverter.ToInt32(message, i);
            i += sizeof(int);
            return (result);

        }
        private Player FindPlayer(int id)
        {
            foreach (Player searchedPlayer in gameCore.otherPlayers)
            {
                if (searchedPlayer.id == id)
                {
                    return searchedPlayer;
                }
            }
            return null;
        }
        public void AnalyzeMessage(byte[] message)
        {
            int i = 0;
            int idClient = GetNextValue(message, ref i);
            int kindMessage;
            while (i < message.Length)
            {
                kindMessage = GetNextValue(message, ref i);
                switch (kindMessage)
                {
                    case (int)KindMessage.Player:
                        {
                            if (gameCore.player.id != idClient)
                            {

                                int kindPlayerAction = GetNextValue(message, ref i);
                                switch (kindPlayerAction)
                                {
                                    case (int)KindPlayerMessages.NewDirection:
                                        {
                                            Player searchedPlayer = FindPlayer(idClient);
                                            searchedPlayer.direction = (Player.Direction)GetNextValue(message, ref i);
                                        }
                                        break;
                                    case (int)KindPlayerMessages.Spawn:
                                        {
                                            Player searchedPlayer = FindPlayer(idClient);

                                            searchedPlayer.X = 20;
                                            searchedPlayer.Y = 20;
                                            searchedPlayer.texture = gameCore.playerTexture;
                                            searchedPlayer.IsDying = false;
                                            searchedPlayer.IsDead = false;
                                        }
                                        break;
                                    case (int)KindPlayerMessages.Death:
                                        {

                                        }
                                        break;
                                    case (int)KindPlayerMessages.PlaceBomb:
                                        {
                                            Player searchedPlayer = FindPlayer(idClient);
                                            if (searchedPlayer.CurrCountBombs != searchedPlayer.maxCountBombs)
                                            {
                                                gameCore.bombs.Add(searchedPlayer.bombFactory.GetBomb(searchedPlayer.bombLevel, new Point(searchedPlayer.X, searchedPlayer.Y)));
                                                searchedPlayer.CurrCountBombs++;
                                            }
                                        }
                                        break;
                                    case (int)KindPlayerMessages.Connect:
                                        {
                                        }
                                        break;
                                    case (int)KindPlayerMessages.Disconnect:
                                        {
                                        }
                                        break;
                                    case (int)KindPlayerMessages.StopWalking:
                                        {
                                            Player searchedPlayer = FindPlayer(idClient);
                                            searchedPlayer.isMoved = false;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case (int)KindMessage.Wall:
                        {
                        }
                        break;
                }
            }
        }
    }
}
