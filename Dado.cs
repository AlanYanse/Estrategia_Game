using System;
using System.Collections.Generic;
using System.Text;

namespace Estrategia_Game
{
    public class Dado
    {

        public int lanzarDado()
        {

            var rand = new Random();

            int aleatorio = rand.Next(6) + 1;


            return aleatorio;
        }
    }

}
