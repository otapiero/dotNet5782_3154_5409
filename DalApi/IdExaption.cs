﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  namespace DO
    {
        [Serializable]
        public class IdExaption : Exception
        {


            public string Mes;
            public IdExaption(string message) : base(message)
            {

            }



            public override string ToString()
            {
                return $"IdNotFoundException: DAL error id\n";
            }


        }
    }
