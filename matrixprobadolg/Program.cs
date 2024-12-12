using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace matrixprobadolg
{
	internal class Program
	{

		static (int[,], int, int) Beolvas(string fajlnev)
		{
			string[] sorok = File.ReadAllLines(fajlnev);
			int N = sorok.Length; // sorok száma.
			int M = sorok[0].Length; // oszlopok száma: az első stringsor hossza!

			int[,] m = new int[N, M]; // hatalmas NxM-es táblázat nullákkal tele.

			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					// m[i, j] = int.Parse(sorok[i][j].ToString());
					m[i, j] = Convert.ToInt32(sorok[i][j].ToString());
				}
			}
			return (m, N, M);
		}

		static bool Van_e(int ez, int[,] m, int N, int M)
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					if (m[i, j] == ez)
					{
						return true;
					}
				}
			}
			return false;
		}
		static int Hany_ilyen_van(int ez, int[,] m, int N, int M)
		{
			int db = 0;
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					if (m[i, j] == ez)
					{
						db++;
					}
				}
			}
			return db;
		}


		static int Osszertek(int[,] m, int N, int M)
		{
			int result = 0;
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					if (m[i, j] % 2 == 0)
					{
						result += m[i, j] / 2;
					}
				}
			}
			return result;
		}

		static (int, int) Elso(int ez, int[,] m, int N, int M)
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					if (m[i, j] == ez)
					{
						return (i, j);
					}
				}

			}
			return (-1, -1);
		}

		static Dictionary<int, int> Statisztika(int[,] m, int N, int M)
		{
			Dictionary<int, int> result = new Dictionary<int, int>();
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					if (result.ContainsKey(m[i, j]))
					{
						result[m[i, j]] += 1;
					}
					else
					{
						result[m[i, j]] = 1;
					}

				}
			}
			return result;
		}

		static int[] Statisztika2(int[,] m, int N, int M)
		{
			int[] result = new int[10];
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < M; j++)
				{
					result[m[i, j]] += 1;
				}
			}
			return result;
		}

		static (int, int) Körbekerített_mező(int[,] m, int N, int M)
		{
			for (int i = 1; i < N - 1; i++) // később kell kezdeni és hamarabb abbahagyni, mivel az első sor és az utolsó sor nem lehet körbekerített!
			{
				for (int j = 1; j < M - 1; j++) // később kell kezdeni és hamarabb abbahagyni, mivel az első oszlop és az utolsó oszlop nem lehet körbekerített!
				{
					if (m[i - 1, j] == 9 && m[i, j - 1] == 9 && m[i + 1, j] == 9 && m[i, j + 1] == 9)
					{
						return (i, j);
					}
				}
			}
			return (-1, -1);
		}


		static void Main(string[] args)
		{

			(int[,] m, int N, int M) = Beolvas("input.txt");

			bool van_e_nulla = Van_e(0, m, N, M);

			Console.WriteLine("1. kérdés: Van-e olyan kincs a pályán, ami semennyit sem ér?");
			if (van_e_nulla)
			{
				Console.WriteLine("1. válasz: Van értéktelen kincs!");
			}
			else
			{
				Console.WriteLine("1. válasz: Nincs értéktelen kincs!");
			}
			Console.WriteLine("2. kérdés: Hány fal van a pályán?");
			int falak_száma = Hany_ilyen_van(9, m, N, M);
			Console.WriteLine($"2. válasz: Összesen {falak_száma} db fal van a pályán.");

			Console.WriteLine("3. kérdés: Mennyit ér az összes pályán található kincs együttvéve?");

			int osszes_kincs_erteke = Osszertek(m, N, M);

			Console.WriteLine($"3. válasz: összesen {osszes_kincs_erteke} értékben van kincs a pályán!");

			(int sor, int oszlop) = Elso(7, m, N, M);
			Console.WriteLine("Add meg az első 7-es sor- és oszlopszámát");
			Console.WriteLine($"4. válasz: Az első hetes itt van: {sor}. sor, {oszlop}.oszlop");

			Console.WriteLine($"5. kérdés: Készíts statisztikát! Melyik számjegyből hány darab található a pályán?");
			Console.WriteLine("5. válasz: szótárral:");
			Dictionary<int, int> statisztika = Statisztika(m, N, M);

			for (int i = 1; i <= 9; i++)
			{
				Console.WriteLine($"{i}: {statisztika[i]} db");
			}

			Console.WriteLine("5. válasz: tömbbel:");
			int[] statisztika2 = Statisztika2(m, N, M);
			for (int i = 0; i <= 9; i++)
			{
				Console.WriteLine($"{i}: {statisztika2[i]} db");
			}
			Console.WriteLine("6. kérdés: Van-e olyan pont a pályán, aminek mind a négy szomszédja fal? Ha igen, add meg ennek a mezőnek a sor- és oszlopszámát!");
			(sor, oszlop) = Körbekerített_mező(m, N, M);
			Console.WriteLine($"6. válasz: Ez a mező van körbekerítve: {sor}. sor, {oszlop}. oszlop");
		}

	}
}
