﻿using Csharp_tictoe_game.Applications.Handlers;
using System.Net;
using System;
using Csharp_tictoe_game.Applications.Interfaces;
using Csharp_tictoe_game.Logging;
using GameDatabase.Mongodb.Handlers;
using Csharp_tictoe_game.GameModels;

namespace Csharp_tictoe_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameLogger logger = new GameLogger();
            var mongodb = new MongoDb();
            //var mongoHandler = new MongoHandler<User>(mongodb.GetDatabase()); 
            IPlayerManager playerManager = new PlayersManager(logger);
            var wsServer = new WsGameServer(IPAddress.Any, port: 8080, playerManager, logger, mongodb);
            wsServer.StartServer();
            logger.Print("Game Server started");
            for (; ; )
            {
                string type = Console.ReadLine();
                if (type.Equals("restart"))
                {
                    logger.Print("Game Server restarting...");
                    wsServer.RestartServer();
                }

                if (type.Equals("shutdown"))
                {
                    logger.Print("Game Server stopping...");
                    wsServer.StopSerever();
                    break;
                }
            }
        }
    }
}
