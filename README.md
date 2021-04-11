# CSP AIMA#

I.	Introduction

L’objectif de ce devoir est de concevoir un CSP, ou problème à satisfaction de contraintes, modélisant un problème bien connu : le jeu du Sudoku, en se référant sur le modèle d’analyse des sentiments vu en cours.

II.	Définition et Fonctionnement du CSP 

•	Définition
Les problèmes de satisfaction de contraintes ou CSP (Constraint Satisfaction Problem) sont des problèmes mathématiques où l'on cherche des états ou des objets satisfaisant un certain nombre de contraintes ou de critères, Ils sont au cœur de la programmation par contraintes, un domaine fournissant des langages de modélisation de problèmes et des outils informatiques les résolvant.
Les algorithmes utilisés pour résoudre des problèmes de satisfaction de contraintes incluent les algorithmes de propagation de contraintes, le retour sur trace (et son évolution non chronologique), l’apprentissage de contraintes (en) et l'algorithme des conflits minimaux (en).
•	Fonctionnement dans le Sudoku
Dans le Sudoku, nous choisissons la cellule sur laquelle nous allons travailler (soit la première n’ayant pas de valeur, soit celle ayant le moins de valeurs possibles).
Tant que cette cellule possède des valeurs possibles, on réalise les actions suivantes : nous choisissons une valeur dans la liste des valeurs possibles (soit la première disponible, soit celle ayant le maximum de contraintes). Si cette valeur n’est pas déjà prise par un de peers de la cellule considérée, nous relançons une backtracking search sur le Sudoku modifié comme suit :
Suppression de la liste des valeurs possibles des peers la valeur attribuée, c’est la propagation de contraintes. 
 	Mise à jour la valeur de la cellule. 
Si la propagation de contraintes n’entraine pas d’erreur, i.e. si aucun des peers ne se retrouve sans valeur possible, nous continuons à approfondir l’arbre. 
Dans le cas contraire, nous supprimons la valeur testée des valeurs possibles de la cellule et on recommence avec une autre valeur. 
La récurrence se termine sur un des 2 cas suivants : 
Soit le Sudoku est complété (renvoie true), on retourne le Sudoku résolu.
Soit le Sudoku ne comporte pas de solution, on retourne null. 


III.	
IV.	Travaux réalises

1.	Modifications apportées au code original
 Adaptation pour prise en compte d'un fichier csv
Nous avons défini une variable _filePath  au-dessus du main() qui pointe vers le fichier csv (sudoku.csv) contenant les sudokus dans le dossier du projet console.

2.	Intégration de Spark

SparkSession fournit un point d'entrée unique pour interagir avec la fonctionnalité sous-jacente de Spark, programmer Spark avec les API DataFrame, Dataset, et le transfert des données du csv dessus, la limitation du nombre de ligne/sudoku à traiter (nombre de lignes).

3.	Initialisation de la SparkSession depuis le main (), avec paramètres sur le nombre de sudokus, de cores et d'instances.

Afin d’effectuer un benchmark sur nos solutions, nous avons utilisés la méthode Sudokures qui fait appelle à Sudokusolution depuis la fonction principale main, ainsi nous avons créée deux Stopwatch pour mesurer le temps d’exécution de la création de la session Spark, du DataFrame, de L’UDF, et la résolution des sudoku.

4.	Intégration du Solver CSP 

Nous avons copié le code du programme (CSPSolver.cs), à la fin de notre solution. 
CSPSolver.cs programme contenant :
Les différentes stratégies de résolutions
La construction du modèle 
Le temps d’exécution de la solution 

V.	Conclusion 

Constraint Satisfaction Problem est un modèle rapide et optimisé pour résoudre les Sudokus avec une différence remarquable sur les niveaux  de difficultés et le nombre de puzzles à résoudre.

Ex :
Niveau Difficile ---------> 80 Puzzles ------> 87,24 ms
Niveau Difficile ---------> 20 Puzzles ------> 254,699 ms 
Niveaux Facile ---------->  40 Puzzles -------> 19,441 ms
Niveaux Facile ---------->  15 Puzzles -------> 32,641 ms
 
La différence sur Global Execution Time quant à elle nous ne pouvons pas conclure, car nous n’avons pas pu bien implémenter les codes.



