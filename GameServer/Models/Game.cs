using GameServer.Models.AbstractFactory;
using GameServer.Models.Decorator;
using GameServer.Models.Facade;
using GameServer.Models.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public class Game
    {
        public ICollection<Player> Players { get; set; }
        public ICollection<Bullet> bullets { get; set; }
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public enum Direction
        {
            Left, Right, Up, Down, Stop
        }
        public Direction _lastDirection { get; set; }
        public Direction _playerDirection { get; set; }
        enum Colour
        {
            Red, Blue, Green
        }
        public int _currentPlayerId;
        public Player CurrentPlayer => _currentPlayerId == 1 ? P1 : P2;
        public bool P1Connected, P2Connected = false;
        public List<Obsticale> obsticaless = new List<Obsticale>();
        public int[] kordinates = new int[20];

        public Obsticale obsR = new ObsticaleFacotry().CreateObsticale("R");
        public Obsticale obsB = new ObsticaleFacotry().CreateObsticale("B");
        public Obsticale obsG = new ObsticaleFacotry().CreateObsticale("G");
        public Obsticale obsatskiras;

        public ShopFacade shop = new ShopFacade();

        Gameboard board = new Gameboard();

        public CHP observer = new CHP();
        public PlayerFactory PlayerFactory = new PlayerFactory();

        public Direction getDefaultPlayerDirection()
        {
            return Direction.Stop;
        }

        public Player CreatePlayer(int id)
        {
            Player p1 = PlayerFactory.GetPlayer();
            if (id == 1)
            {
                p1.PosX = 20;
                p1.PosY = 50;
                p1.speed = 10;
            }else
            {
                p1.PosX = 200;
                p1.PosY = 200;
                p1.speed = 10;
            }
            return p1;
        }
      

    }
}
