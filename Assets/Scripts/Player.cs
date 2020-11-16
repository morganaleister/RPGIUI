using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace Scripts
{
    public class Player : BaseObject
    {

        public string PlayerName { get; set; }
        public Achievement[] Achievements { get; set; }
        public Character Avatar { get; set; }
        public Character[] Characters { get; set; }


        public Player(string playerName) => PlayerName = playerName;



    }
}