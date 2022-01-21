using System;
using System.Collections.Generic;
using System.Text;

namespace Estrategia_Game
{
    public class Jugador
    {
		private int id;
		private string nombre;
		private int cant_colonias;
		private Dado dadito = new Dado();

		public Jugador(int id, string nombre)
		{

			this.id = id;
			this.nombre = nombre;
			this.cant_colonias = 2;

		}

		public void setColonia(bool resultado)
		{
			if(resultado == true)
			{
				this.cant_colonias += 1;
			}
			else
			{
				this.cant_colonias -= 1;
			}
		}

		public int getId()
		{

			return this.id;
		}

		public string getNombre()
		{

			return this.nombre;
		}

		public int getColonias()
		{


			return this.cant_colonias;

		}

		public bool atacar(Jugador o_jugador_defensor)
		{

			bool gana = false;
			int num_atacante = dadito.lanzarDado();
			string opcion;

			Console.WriteLine("Te ha salido " + num_atacante);
			Console.WriteLine("====================================================");
			Console.WriteLine("Jugador defensor, que deseas hacer?");
			Console.WriteLine("====================================================");
			
			do
			{
				Console.WriteLine("1- Defender");
				Console.WriteLine("2- Ceder");
				opcion = Console.ReadLine();

				if (opcion == "1")
				{
					Console.WriteLine("Jugador " + o_jugador_defensor.getNombre() + " presionar enter para lanzar el dadito");
					Console.ReadLine();
					int num_defensor = dadito.lanzarDado();
					Console.WriteLine("Te ha salido " + num_defensor);

					if (num_atacante > num_defensor)
					{
						gana = true;

					
					}else if(num_atacante < num_defensor)
					{

						gana = false;

					}
					else
					{
						gana = false;
					}
				}
				else if(opcion == "2")
				{
					gana = true;
				}
				else
				{
					Console.WriteLine("La opcion ingresada no es correcta");
				}



			} while (opcion != "1" && opcion != "2");


			return gana;

			
		}



	}
}
