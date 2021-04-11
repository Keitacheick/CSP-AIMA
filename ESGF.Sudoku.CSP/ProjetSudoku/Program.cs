using System;
using System.Collections.Generic;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.Spark.Sql;
using ProjetSudokuML.Model;
using System.Diagnostics;
using aima.core.search.csp;
using Sudoku.Core;
using System.Collections.Immutable;
using Sudoku.CSPSolver;
using Microsoft.Azure.DataLake.Store;

namespace ProjetSudoku
{
   
        public static class Program
        {
            //Path du fichier csv avec 1 000 000 sudokus.
            static readonly string _filePath = System.IO.Path.Combine(@"C:\Users\kboub\Desktop\test_code_bench_senti\ProjetSudoku\ProjetSudokuML.ConsoleApp\sudoku.csv", "sudoku.csv");

            public static void Main()
            {
                //temps d'execution global (chargement du CSV + création DF et sparksession)
                var watch = new System.Diagnostics.Stopwatch();
                var watch1 = new System.Diagnostics.Stopwatch();

                watch.Start();

                //Appel de la méthode, spark session avec 1 noyau et 1 instance, 500 sudokus à résoudre
                Sudokures("1", "1", 500);

                watch.Stop();
                watch1.Start();

                //Appel de la méthode, spark session avec 1 noyau et 4 instance, 500 sudokus à résoudre
                Sudokures("1", "4", 500);

                watch1.Stop();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Global Execution (CSV + DF + SparkSession) Time with 1 core and 1 instance: {watch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Global Execution (CSV + DF + SparkSession) Time with 1 core and 4 instances: {watch1.ElapsedMilliseconds} ms");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

            }

            //Méthode qui est appelée depuis le main pour lancer une session spark avec un nombre
            //de noyaux et d'instances différents et lancer la résolution du soduku grace à la méthode Sudokusolution().
            private static void Sudokures(string cores, string nodes, int nrows)
            {
                // Initialisation de la session Spark
                SparkSession spark = SparkSession
                    .Builder()
                    //.AppName("Resolution of " + nrows + " sudokus using  with " + cores + " cores and " + nodes + " instances")
                    .Config("spark.executor.cores", cores)
                    .Config("spark.executor.instances", nodes)
                    .GetOrCreate();

                // Intégration du csv dans un dataframe
                DataFrame df = spark
                    .Read()
                    .Option("header", true)
                    .Option("inferSchema", true)
                    .Csv(_filePath);

                //limit du dataframe avec un nombre de ligne prédéfini lors de l'appel de la fonction
                DataFrame df2 = df.Limit(nrows);

                //Watch seulement pour la résolution des sudokus
                var watch2 = new System.Diagnostics.Stopwatch();
                watch2.Start();

                // Création de la spark User Defined Function
                spark.Udf().Register<string, string>(
                    "SukoduUDF",
                    (sudoku) => Sudokusolution(sudoku));

                // Appel de l'UDF dans un nouveau dataframe spark qui contiendra les résultats aussi
                df2.CreateOrReplaceTempView("Resolved");
                DataFrame sqlDf = spark.Sql("SELECT Sudokus, SukoduUDF(Sudokus) as Resolution from Resolved");
                sqlDf.Show();

                watch2.Stop();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"Execution Time for " + nrows + " sudoku resolution with " + cores + " core and " + nodes + " instance: " + watch2.ElapsedMilliseconds + " ms");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                spark.Stop();

            }

        private static string Sudokusolution(string sudoku)
        {
            throw new NotImplementedException();
        }

        public class CSPSolver : ISudokuSolver
        {
            public CSPSolver()
            {
                // Définition d'une stratégie de résolution
                var objStrategyInfo = new CSPStrategyInfo();
                objStrategyInfo.EnableLCV = true;
                objStrategyInfo.Inference = CSPInference.ForwardChecking;
                objStrategyInfo.Selection = CSPSelection.MRVDeg;
                objStrategyInfo.StrategyType = CSPStrategy.ImprovedBacktrackingStrategy;
                objStrategyInfo.MaxSteps = 5000;
                _Strategy = objStrategyInfo.GetStrategy();
            }


            private SolutionStrategy _Strategy;


            public Core.Sudoku Solve(Core.Sudoku s)
            {
                //Construction du CSP

                var objCSP = SudokuCSPHelper.GetSudokuCSP(s);

                // Résolution du Sudoku
                var objChrono = Stopwatch.StartNew();
                var assignation = _Strategy.solve(objCSP);
                Console.WriteLine($"Pure solve Time : {objChrono.Elapsed.TotalMilliseconds} ms");


                //Traduction du Sudoku
                SudokuCSPHelper.SetValuesFromAssignment(assignation, s);

                return s;
            }
        }



    }

    public interface ISudokuSolver
    {
        Core.Sudoku Solve(Core.Sudoku s);
    }
}

