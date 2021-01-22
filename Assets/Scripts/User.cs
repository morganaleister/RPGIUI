using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;


namespace Scripts
{
    public class User : BaseObject
    {
        public Achievement[] Achievements { get; set; }
        public Character Avatar { get; set; }
        public Character[] Characters { get; set; }


        public User(string userName) : base(userName) { }



    }
}