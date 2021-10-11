using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Estrategia_Game
{
    public class Game
    {
		private string opcion;
		private int cant_jugadores, cant_rondas;
		private bool continuar = true;
		//private ArrayList jugadores = new ArrayList(); Al principio quise usar un ArrayList pero no me sirvió
		List<Jugador> lista_jugadores = new List<Jugador>();


		public Game()
		{
			Console.Clear();
			Console.Title = "Estrategy";
			run();
		}

		public void run()
		{

			menuInicio();

		}

		public void menuInicio()
		{


			Console.WriteLine("====================BIENVENIDOS A ESTRATEGY====================");


			do
			{

				Console.WriteLine("Ingresar la opcion a realizar: ");
				Console.WriteLine("1- Jugar");
				Console.WriteLine("2- Salir");
				opcion = Console.ReadLine();

				if (opcion == "1")
				{

					Console.WriteLine("Se eligió jugar");

					pantallaCrearPartida();
					jugarBuclePrincipal();

				}
				else if (opcion == "2")
				{

					Console.WriteLine("Se eligió salir");

				}
				else
				{


					Console.Clear();
					Console.WriteLine("La opción elegida no está contemplada");
					Console.WriteLine("Vuelve a elegir 1 o 2");


				}



			} while (opcion != "1" && opcion != "2");



		}

		public void pantallaCrearPartida()
		{

			Console.Clear();

			Console.WriteLine("Ingresar la cantidad de jugadores");

			cant_jugadores = int.Parse(Console.ReadLine());

			Console.WriteLine("La cantidad de jugadores que disputarán el Deathmatch es: " + cant_jugadores);

			asignarJugadores();

		}

		public void asignarJugadores()
		{

			
			for (int i = 0; i < cant_jugadores; i++)
			{

				Console.WriteLine("Ingresar nombre del jugador " + (i + 1));

				string nombre_jugador = Console.ReadLine();

				lista_jugadores.Add(new Jugador(i + 1, nombre_jugador));


			}

			Console.WriteLine("Presionar una tecla para continuar");

			Console.ReadLine();

			

		}

		public void jugarBuclePrincipal()
		{

			
			string eleccion;

			Console.Clear();

			//---------------------- Acá se empieza a jugar el bucle pricipal -------------------------------------

			pintarDashBoard();

			do
			{

				rondaDinova(); // Método donde se ejecuta las rondas del juego



				

			} while (cant_jugadores > 1 && continuar == true);

		}

		public void pintarDashBoard()
		{
			Console.WriteLine("==== ID ==== NOMBRE ==== COLONIAS ===");

			foreach (Jugador player in lista_jugadores)
			{
				Console.WriteLine("     " + player.getId() + "        " + player.getNombre() + "        " + player.getColonias());
				
			}


		}

		public void rondaDinova()
		{

			do
			{

								
				for(int i = 0; i < cant_jugadores; i++)
				{
					turno(lista_jugadores[i]);
					pintarDashBoard();
				}

				cant_rondas++;

				Console.WriteLine("ha transcurrido " + cant_rondas + " rondas");

				//continuar = false;

							   

			} while (cant_jugadores > 1);
			
				
			
		}

		public void turno(Jugador el_jugador_de_turno) // Para tener referencia de los jugadores se les pasa el objeto Jugador por parámetro
		{

			string opcion_turno;
			bool resultado;

			Console.WriteLine("Es el turno del jugador " + el_jugador_de_turno.getId() + " " + el_jugador_de_turno.getNombre());

			// Acá debería ir en un do while por si colocan cosas que no son

			do
			{

							

				Console.WriteLine("Que deseas hacer?");

				Console.WriteLine("1- Atacar");
				Console.WriteLine("2- Pasar de todo");

				opcion_turno = Console.ReadLine();

				

				if (opcion_turno == "1") // ATACAR
				{

					string jugador_defensor;
					
					// Se tendrá que ejecutar el método el_jugador_de_turno.atacar();
					// Visualizar el dashboar
					
																	

					visualizarDashboardDeTurno(el_jugador_de_turno); // Este es un dashboard distinto al del comienzo.

					Console.WriteLine("====================================================");
					Console.WriteLine("Ingresar el nombre del jugador que se quiere atacar");
					Console.WriteLine("====================================================");

					jugador_defensor = Console.ReadLine(); // Después tengo que vincular esto al objeto getNombre() y pasarlo como parámetro al método atacar

					Jugador o_jugador_defensor = lista_jugadores.Find(p => p.getNombre().Equals(jugador_defensor)); // Uso del método find para obtener el objeto del jugador al cual se elige atacar

					Console.WriteLine("Has elegido atacar a " + o_jugador_defensor.getNombre());

					Console.WriteLine("====================================================");
					Console.WriteLine("Jugador " + el_jugador_de_turno.getId() + " " + el_jugador_de_turno.getNombre() + " presionar enter para tirar el dadito");
					Console.WriteLine("====================================================");
					Console.ReadLine();
					resultado = el_jugador_de_turno.atacar(o_jugador_defensor); // el método atacar() devuelve true si el atacante gana o false si el atacante pierde.

					Console.WriteLine(resultado);

					//---------------- A continuación el bloque de código para sumar o restar colonias a los jugadores ---------------

					if(resultado == true)
					{
						Console.WriteLine("Hay que sumarle una colonia al jugador de turno y restarle una al jugador defensor");
						el_jugador_de_turno.setColonia(resultado);

						resultado = false;
						o_jugador_defensor.setColonia(resultado);


						if (o_jugador_defensor.getColonias() == 0)
						{
							lista_jugadores.Remove(o_jugador_defensor);
							cant_jugadores -= 1;
						}

						
					
					}

					else
					{
						Console.WriteLine("Hay que sumarle una colonia al jugador defensor y restarle una al jugador de turno");
						el_jugador_de_turno.setColonia(resultado);

						resultado = true;
						o_jugador_defensor.setColonia(resultado);

						if (el_jugador_de_turno.getColonias() == 0)
						{
							lista_jugadores.Remove(el_jugador_de_turno);
							cant_jugadores -= 1;
						}
					}


				}
				else if (opcion_turno == "2") // PASAR DE TODO
				{
					// Se tendrá que ejecutar el método el_jugador_de_turno.pasarDeTodo(); 
				}
				else
				{
					Console.WriteLine("La opcion ingresada no es correcta");
				}




			} while (opcion_turno != "1" && opcion_turno != "2");

						
		
		}

		public void visualizarDashboardDeTurno(Jugador player)
		{

			Console.WriteLine("==== ID ==== NOMBRE ==== COLONIAS ===");

			
			for(int i = 0; i < lista_jugadores.Count; i++)
			{
				if(lista_jugadores[i].getNombre() != player.getNombre()) // Se filtra para que no aparezca el jugador de turno en el dashboard
				{
					Console.WriteLine(lista_jugadores[i].getId() + lista_jugadores[i].getNombre() + lista_jugadores[i].getColonias());
				}
				
				
			}


		}

	}
}
