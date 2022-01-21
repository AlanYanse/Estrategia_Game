using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Estrategia_Game
{
    public class Game
    {
		
		private int cant_jugadores, cant_rondas;
		
		List<Jugador> lista_jugadores = new List<Jugador>();

//-------------- CONSTRUCTOR -------------------

		public Game()
		{
			Console.Clear();
			Console.Title = "Estrategya";
			run();
		}

//----------------------------------------------
		
		public void run()
		{

			menuInicio();

		}


//----------------------------------------------

		public void menuInicio()
		{
			string opcion;


			presentacion();


			do
			{

				Console.WriteLine("Ingresar la opcion a realizar: ");
				Console.WriteLine("1- Jugar");
				Console.WriteLine("2- Salir");
				opcion = Console.ReadLine();

				if (opcion == "1")
				{


					pantallaCrearPartida();
					jugarBuclePrincipal();

				}
				else if (opcion == "2")
				{

					Console.WriteLine("Se eligió salir");
					
					Console.WriteLine("Presionar enter para cerrar la aplicación");

					Environment.Exit(0);
				}
				else
				{


					Console.Clear();
					Console.WriteLine("La opción elegida no está contemplada");
					Console.WriteLine("Vuelve a elegir 1 o 2");


				}



			} while (opcion != "1" && opcion != "2");



		}


//----------------------------------------------

		public void pantallaCrearPartida()
		{

			string cadena;

			Console.Clear();

			do
			{
								

				Console.WriteLine("Ingresar la cantidad de jugadores");

				cadena = Console.ReadLine();

				if (Char.IsNumber(cadena, 0) == true) // Método IsNumber es para saber si un char o string es número.
				{
					cant_jugadores = int.Parse(cadena);
				}
				else
				{
					Console.WriteLine("ingresar un valor numerico. presionar enter para continuar");
					Console.ReadLine();
					pantallaCrearPartida();
				}
				
			} while(cant_jugadores < 2); // se va repetir esto hasta que el usuario ingrese un valor numérico y mayor o igual a 2.

			Console.WriteLine("La cantidad de jugadores que disputarán el Deathmatch es: " + cant_jugadores);

			asignarJugadores(); // Finalmente si el usuario ingresa bien los valores se pueden agregar los jugadores


		}

		//-------- Método que permite agregar los jugadores a la partida mediante una List -------------

		public void asignarJugadores()
		{

			
			for (int i = 0; i < cant_jugadores; i++)
			{

				Console.WriteLine("Ingresar nombre del jugador " + (i + 1));

				string nombre_jugador = Console.ReadLine();

				lista_jugadores.Add(new Jugador(i + 1, nombre_jugador));


			}

			Console.WriteLine("presionar enter para continuar");

			Console.ReadLine();

			

		}


//----------------------------------------------

		public void jugarBuclePrincipal()
		{
						
			string opcion;

			Console.Clear();

			//---------------------- Acá se empieza a jugar el bucle pricipal -------------------------------------

			pintarDashBoard(); // Es la tabla donde puede visualizarse la información principal del juego
						
			rondaDinova(); // Método donde se ejecuta las rondas del juego

			Console.WriteLine("Fin del juego");

			Console.WriteLine("Volver a jugar S/N");

			opcion = Console.ReadLine().ToLower();

			if(opcion == "s")
			{

				menuInicio();

			}else
			{

				Environment.Exit(0);
			}


		}


//----------------------------------------------

		public void pintarDashBoard()
		{
			Console.WriteLine("===== ID ======== NOMBRE ==== COLONIAS ========");

			foreach (Jugador player in lista_jugadores)
			{
				Console.WriteLine("       " + player.getId() + "          " + player.getNombre() + "          " + player.getColonias());
				
			}


		}

		
//----------------------------------------------

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

										   

			} while (cant_jugadores > 1);

			Jugador jugador_ganador = lista_jugadores.Find(p => p.getColonias() > 0);

			Console.WriteLine("Felicitaciones jugador " + jugador_ganador.getNombre() + " has ganado el Deathmatch");

			lista_jugadores.Clear();
			
		}

		
//----------------------------------------------

		public void turno(Jugador el_jugador_de_turno) // Para tener referencia de los jugadores se les pasa el objeto Jugador por parámetro
		{

			string opcion_turno;
			bool resultado;
			bool existe = true;

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

					do
					{

						visualizarDashboardDeTurno(el_jugador_de_turno); // Este es un dashboard distinto al del comienzo.

						Console.WriteLine("====================================================");
						Console.WriteLine("Ingresar el nombre del jugador que se quiere atacar");
						Console.WriteLine("====================================================");

						jugador_defensor = Console.ReadLine().ToLower(); // Después tengo que vincular esto al objeto getNombre() y pasarlo como parámetro al método atacar



						if (lista_jugadores.Exists(p => p.getNombre().Equals(jugador_defensor)))
						{

							existe = true;

							Jugador o_jugador_defensor = lista_jugadores.Find(p => p.getNombre().Equals(jugador_defensor)); // Uso del método find para obtener el objeto del jugador al cual se elige atacar

							Console.WriteLine("Has elegido atacar a " + o_jugador_defensor.getNombre());

							Console.WriteLine("====================================================");
							Console.WriteLine("Jugador " + el_jugador_de_turno.getId() + " " + el_jugador_de_turno.getNombre() + " presionar enter para tirar el dadito");
							Console.WriteLine("====================================================");
							Console.ReadLine();
							resultado = el_jugador_de_turno.atacar(o_jugador_defensor); // el método atacar() devuelve true si el atacante gana o false si el atacante pierde.

							//Console.WriteLine(resultado);

							//---------------- A continuación el bloque de código para sumar o restar colonias a los jugadores ---------------

							if (resultado == true)
							{
								Console.WriteLine("Resultado Parcial");
								el_jugador_de_turno.setColonia(resultado);

								resultado = false;
								o_jugador_defensor.setColonia(resultado);


								if (o_jugador_defensor.getColonias() == 0)
								{
									lista_jugadores.Remove(o_jugador_defensor);
									Console.WriteLine("El jugador " + o_jugador_defensor.getNombre() + " ha sido eliminado");
									cant_jugadores -= 1;
								}


							}

							else
							{
								
								Console.WriteLine("Resultado Parcial");
								
								el_jugador_de_turno.setColonia(resultado);

								resultado = true;
								o_jugador_defensor.setColonia(resultado);

								if (el_jugador_de_turno.getColonias() == 0)
								{
									lista_jugadores.Remove(el_jugador_de_turno);
									Console.WriteLine("El jugador " + el_jugador_de_turno.getNombre() + " ha sido eliminado");
									cant_jugadores -= 1;
								}
							}

						}
						else
						{
							Console.WriteLine("El nombre ingresado no es válido");
							existe = false;
						}


					} while (existe == false);												

					

				}
				else if (opcion_turno == "2") // PASAR DE TODO
				{
					Console.WriteLine("Elegiste pasar de todo");
					Console.WriteLine("Presionar enter para el siguiente turno");
					Console.ReadLine();

				}
				else
				{
					Console.WriteLine("La opcion ingresada no es correcta");
				}




			} while (opcion_turno != "1" && opcion_turno != "2");

						
		
		}


//----------------------------------------------

		public void visualizarDashboardDeTurno(Jugador player)
		{

			Console.WriteLine("===== ID ======== NOMBRE ==== COLONIAS ========");

			
			for(int i = 0; i < lista_jugadores.Count; i++)
			{
				if(lista_jugadores[i].getNombre() != player.getNombre()) // Se filtra para que no aparezca el jugador de turno en el dashboard
				{
					Console.WriteLine("       " + lista_jugadores[i].getId() + "          " + lista_jugadores[i].getNombre() + "       " +  lista_jugadores[i].getColonias());
				}
				
				
			}


			


		}



//----------------------------------------------

						public void presentacion(){
							

							Console.Clear();
							

							Console.ForegroundColor = ConsoleColor.Green;
						

							Console.WriteLine("");

							Console.WriteLine("");

							Console.WriteLine("         8888888888 .d8888b.888888888888888888b.        d8888888888888888888888888 .d8888b.Y88b   d88P    d8888");
						
							Console.WriteLine("         888       d88P  Y88b   888    888   Y88b      d88888    888    888       d88P  Y88bY88b d88P    d88888");
						
							Console.WriteLine("         888       Y88b         888    888    888     d88P888    888    888       888    888 Y88o88P    d88P888");
						
							Console.WriteLine("         8888888     Y888b      888    888   d88P    d88P 888    888    8888888   888         Y888P    d88P 888");
						
							Console.WriteLine("         888            Y88b    888    8888888P     d88P  888    888    888       888  88888   888    d88P  888");
						
							Console.WriteLine("         888              888   888    888 T88b    d88P   888    888    888       888    888   888   d88P   888");
						
							Console.WriteLine("         888       Y88b  d88P   888    888  T88b  d8888888888    888    888       Y88b  d88P   888  d8888888888");
						
							Console.WriteLine("         8888888888  Y8888P     888    888   T88bd88P     888    888    8888888888  Y8888P88   888 d88P     888");
							
							Console.ForegroundColor = ConsoleColor.Magenta;
							
							Console.WriteLine("");

							Console.WriteLine("                                                                                                          V^1.0");
							
							Console.WriteLine("                                                                                                     Alan Yanse");
							
							Console.WriteLine("                                                                                                     21/01/2022");
							
							Console.WriteLine("");
							
							Console.WriteLine("");
							
							Console.ResetColor();




						}

	


	     }





}





























