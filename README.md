I.	Introduction

L�objectif de ce devoir est de concevoir un CSP, ou probl�me � satisfaction de contraintes, mod�lisant un probl�me bien connu : le jeu du Sudoku, en se r�f�rant sur le mod�le d�analyse des sentiments vu en cours.

II.	D�finition et Fonctionnement du CSP 

�	D�finition
Les probl�mes de satisfaction de contraintes ou CSP (Constraint Satisfaction Problem) sont des probl�mes math�matiques o� l'on cherche des �tats ou des objets satisfaisant un certain nombre de contraintes ou de crit�res, Ils sont au c�ur de la programmation par contraintes, un domaine fournissant des langages de mod�lisation de probl�mes et des outils informatiques les r�solvant.
Les algorithmes utilis�s pour r�soudre des probl�mes de satisfaction de contraintes incluent les algorithmes de propagation de contraintes, le retour sur trace (et son �volution non chronologique), l�apprentissage de contraintes (en) et l'algorithme des conflits minimaux (en).
�	Fonctionnement dans le Sudoku
Dans le Sudoku, nous choisissons la cellule sur laquelle nous allons travailler (soit la premi�re n�ayant pas de valeur, soit celle ayant le moins de valeurs possibles).
Tant que cette cellule poss�de des valeurs possibles, on r�alise les actions suivantes : nous choisissons une valeur dans la liste des valeurs possibles (soit la premi�re disponible, soit celle ayant le maximum de contraintes). Si cette valeur n�est pas d�j� prise par un de peers de la cellule consid�r�e, nous relan�ons une backtracking search sur le Sudoku modifi� comme suit :
Suppression de la liste des valeurs possibles des peers la valeur attribu�e, c�est la propagation de contraintes. 
 	Mise � jour la valeur de la cellule. 
Si la propagation de contraintes n�entraine pas d�erreur, i.e. si aucun des peers ne se retrouve sans valeur possible, nous continuons � approfondir l�arbre. 
Dans le cas contraire, nous supprimons la valeur test�e des valeurs possibles de la cellule et on recommence avec une autre valeur. 
La r�currence se termine sur un des 2 cas suivants : 
Soit le Sudoku est compl�t� (renvoie true), on retourne le Sudoku r�solu.
Soit le Sudoku ne comporte pas de solution, on retourne null. 


III.	
IV.	Travaux r�alises

1.	Modifications apport�es au code original
 Adaptation pour prise en compte d'un fichier csv
Nous avons d�fini une variable _filePath  au-dessus du main() qui pointe vers le fichier csv (sudoku.csv) contenant les sudokus dans le dossier du projet console.

2.	Int�gration de Spark

SparkSession fournit un point d'entr�e unique pour interagir avec la fonctionnalit� sous-jacente de Spark, programmer Spark avec les API DataFrame, Dataset, et le transfert des donn�es du csv dessus, la limitation du nombre de ligne/sudoku � traiter (nombre de lignes).

3.	Initialisation de la SparkSession depuis le main (), avec param�tres sur le nombre de sudokus, de cores et d'instances.

Afin d�effectuer un benchmark sur nos solutions, nous avons utilis�s la m�thode Sudokures qui fait appelle � Sudokusolution depuis la fonction principale main, ainsi nous avons cr��e deux Stopwatch pour mesurer le temps d�ex�cution de la cr�ation de la session Spark, du DataFrame, de L�UDF, et la r�solution des sudoku.

4.	Int�gration du Solver CSP 

Nous avons copi� le code du programme (CSPSolver.cs), � la fin de notre solution. 
CSPSolver.cs programme contenant :
Les diff�rentes strat�gies de r�solutions
La construction du mod�le 
Le temps d�ex�cution de la solution 

V.	Conclusion 

Constraint Satisfaction Problem est un mod�le rapide et optimis� pour r�soudre les Sudokus avec une diff�rence remarquable sur les niveaux  de difficult�s et le nombre de puzzles � r�soudre.

Ex :
Niveau Difficile ---------> 80 Puzzles ------> 87,24 ms
Niveau Difficile ---------> 20 Puzzles ------> 254,699 ms 
Niveaux Facile ---------->  40 Puzzles -------> 19,441 ms
Niveaux Facile ---------->  15 Puzzles -------> 32,641 ms
 
La diff�rence sur Global Execution Time quant � elle nous ne pouvons pas conclure, car nous n�avons pas pu bien impl�menter les codes.



